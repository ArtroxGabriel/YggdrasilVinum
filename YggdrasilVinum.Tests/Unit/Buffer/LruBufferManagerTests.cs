using FluentAssertions;
using Moq;
using Serilog;
using YggdrasilVinum.Buffer;
using YggdrasilVinum.Models;
using YggdrasilVinum.Storage;

namespace YggdrasilVinum.Tests.Unit.Buffer;

public sealed class LruBufferManagerTests : IDisposable
{
    private readonly LruBufferManager _bufferManager;
    private readonly Mock<IFileManager> _fileManagerMock;

    public LruBufferManagerTests()
    {
        // Setup Serilog for testing
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .CreateLogger();

        _fileManagerMock = new Mock<IFileManager>();
        _bufferManager = new LruBufferManager(_fileManagerMock.Object);
    }

    public void Dispose()
    {
        _bufferManager.Dispose();
        Log.CloseAndFlush();
    }

    private static Page CreateTestPage(ulong pageId, int recordCount = 5)
    {
        var records = new WineRecord[recordCount];
        for (var i = 0; i < recordCount; i++)
            records[i] = new WineRecord((int)pageId * 100 + i, $"Wine {pageId}-{i}", 2009, WineType.Rose);

        return new Page(pageId, records);
    }

    [Fact]
    public async Task InitializeAsync_ShouldReturnSuccess()
    {
        // Act
        var result = await _bufferManager.InitializeAsync();

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.GetValueOrThrow().Should().Be(YggdrasilVinum.Models.Unit.Value);
    }

    [Fact]
    public async Task LoadPageAsync_PageNotInBuffer_ShouldLoadFromFileManager()
    {
        // Arrange
        const ulong pageId = 1;
        var expectedPage = CreateTestPage(pageId);

        _fileManagerMock.Setup(f => f.PageExistsAsync(pageId))
            .ReturnsAsync(Result<bool, StoreError>.Success(true));
        _fileManagerMock.Setup(f => f.ReadPageAsync(pageId))
            .ReturnsAsync(Result<Page, StoreError>.Success(expectedPage));

        // Act
        var result = await _bufferManager.LoadPageAsync(pageId);

        // Assert
        result.IsSuccess.Should().BeTrue();
        var page = result.GetValueOrThrow();
        page.PageId.Should().Be(pageId);
        page.Content.Should().BeEquivalentTo(expectedPage.Content);

        _fileManagerMock.Verify(f => f.PageExistsAsync(pageId), Times.Once);
        _fileManagerMock.Verify(f => f.ReadPageAsync(pageId), Times.Once);
    }

    [Fact]
    public async Task LoadPageAsync_PageInBuffer_ShouldReturnFromBuffer()
    {
        // Arrange
        const ulong pageId = 1;
        var expectedPage = CreateTestPage(pageId);

        _fileManagerMock.Setup(f => f.PageExistsAsync(pageId))
            .ReturnsAsync(Result<bool, StoreError>.Success(true));
        _fileManagerMock.Setup(f => f.ReadPageAsync(pageId))
            .ReturnsAsync(Result<Page, StoreError>.Success(expectedPage));

        // Load page first time
        await _bufferManager.LoadPageAsync(pageId);

        // Act - Load same page again
        var result = await _bufferManager.LoadPageAsync(pageId);

        // Assert
        result.IsSuccess.Should().BeTrue();
        var page = result.GetValueOrThrow();
        page.PageId.Should().Be(pageId);

        // Should only be called once (first load)
        _fileManagerMock.Verify(f => f.PageExistsAsync(pageId), Times.Once);
        _fileManagerMock.Verify(f => f.ReadPageAsync(pageId), Times.Once);
    }

    [Fact]
    public async Task LoadPageAsync_FileManagerError_ShouldReturnError()
    {
        // Arrange
        const ulong pageId = 1;
        var fileError = new StoreError("File not found");

        _fileManagerMock.Setup(f => f.PageExistsAsync(pageId))
            .ReturnsAsync(Result<bool, StoreError>.Error(fileError));

        // Act
        var result = await _bufferManager.LoadPageAsync(pageId);

        // Assert
        result.IsError.Should().BeTrue();
        var error = result.GetErrorOrThrow();
        error.Message.Should().Contain("Failed to check if page exists");
    }

    [Fact]
    public async Task LoadPageAsync_BufferFull_ShouldEvictLru()
    {
        // Arrange - Buffer capacity is 2
        var page1 = CreateTestPage(1);
        var page2 = CreateTestPage(2);
        var page3 = CreateTestPage(3);

        // Setup file manager for all pages
        foreach (var page in new[] { page1, page2, page3 })
        {
            _fileManagerMock.Setup(f => f.PageExistsAsync(page.PageId))
                .ReturnsAsync(Result<bool, StoreError>.Success(true));
            _fileManagerMock.Setup(f => f.ReadPageAsync(page.PageId))
                .ReturnsAsync(Result<Page, StoreError>.Success(page));
            _fileManagerMock.Setup(f => f.WritePageAsync(It.Is<Page>(p => p.PageId == page.PageId)))
                .ReturnsAsync(Result<YggdrasilVinum.Models.Unit, StoreError>.Success(YggdrasilVinum.Models.Unit.Value));
        }

        // Load first two pages
        await _bufferManager.LoadPageAsync(1);
        await _bufferManager.LoadPageAsync(2);

        // Act - Load third page (should evict page 1 as LRU)
        var result = await _bufferManager.LoadPageAsync(3);

        // Assert
        result.IsSuccess.Should().BeTrue();

        // Page 1 should have been written to file manager during eviction
        _fileManagerMock.Verify(f => f.WritePageAsync(It.Is<Page>(p => p.PageId == 1)), Times.Once);
    }

    [Fact]
    public async Task LoadPageAsync_LruOrder_ShouldUpdateCorrectly()
    {
        // Arrange
        var page1 = CreateTestPage(1);
        var page2 = CreateTestPage(2);
        var page3 = CreateTestPage(3);

        // Setup file manager
        foreach (var page in new[] { page1, page2, page3 })
        {
            _fileManagerMock.Setup(f => f.PageExistsAsync(page.PageId))
                .ReturnsAsync(Result<bool, StoreError>.Success(true));
            _fileManagerMock.Setup(f => f.ReadPageAsync(page.PageId))
                .ReturnsAsync(Result<Page, StoreError>.Success(page));
            _fileManagerMock.Setup(f => f.WritePageAsync(It.Is<Page>(p => p.PageId == page.PageId)))
                .ReturnsAsync(Result<YggdrasilVinum.Models.Unit, StoreError>.Success(YggdrasilVinum.Models.Unit.Value));
        }

        // Load pages 1 and 2
        await _bufferManager.LoadPageAsync(1);
        await _bufferManager.LoadPageAsync(2);

        // Access page 1 again (should move it to front of LRU)
        await _bufferManager.LoadPageAsync(1);

        // Act - Load page 3 (should evict page 2, not page 1)
        await _bufferManager.LoadPageAsync(3);

        // Assert - Page 2 should be evicted (not page 1)
        _fileManagerMock.Verify(f => f.WritePageAsync(It.Is<Page>(p => p.PageId == 2)), Times.Once);
        _fileManagerMock.Verify(f => f.WritePageAsync(It.Is<Page>(p => p.PageId == 1)), Times.Never);
    }

    [Fact]
    public async Task PutPageAsync_NewPage_ShouldAddToBuffer()
    {
        // Arrange
        var page = CreateTestPage(1);

        // Act
        var result = await _bufferManager.PutPageAsync(page);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.GetValueOrThrow().Should().Be(YggdrasilVinum.Models.Unit.Value);
    }

    [Fact]
    public async Task PutPageAsync_ExistingPage_ShouldReturnSuccess()
    {
        // Arrange
        var page = CreateTestPage(1);
        await _bufferManager.PutPageAsync(page);

        // Act - Try to put same page again
        var result = await _bufferManager.PutPageAsync(page);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task PutPageAsync_BufferFull_ShouldEvictLru()
    {
        // Arrange
        var page1 = CreateTestPage(1);
        var page2 = CreateTestPage(2);
        var page3 = CreateTestPage(3);

        // Setup file manager to handle eviction
        _fileManagerMock.Setup(f => f.WritePageAsync(It.Is<Page>(p => p.PageId == 1)))
            .ReturnsAsync(Result<YggdrasilVinum.Models.Unit, StoreError>.Success(YggdrasilVinum.Models.Unit.Value));

        // Fill buffer
        await _bufferManager.PutPageAsync(page1);
        await _bufferManager.PutPageAsync(page2);

        // Act - Add third page (should evict page 1)
        var result = await _bufferManager.PutPageAsync(page3);

        // Assert
        result.IsSuccess.Should().BeTrue();
        _fileManagerMock.Verify(f => f.WritePageAsync(It.Is<Page>(p => p.PageId == 1)), Times.Once);
    }

    [Fact]
    public async Task FlushPageAsync_PageExists_ShouldWriteToFileManager()
    {
        // Arrange
        const ulong pageId = 1;
        var page = CreateTestPage(pageId);

        _fileManagerMock.Setup(f => f.WritePageAsync(page))
            .ReturnsAsync(Result<YggdrasilVinum.Models.Unit, StoreError>.Success(YggdrasilVinum.Models.Unit.Value));

        await _bufferManager.PutPageAsync(page);

        // Act
        var result = await _bufferManager.FlushPageAsync(pageId);

        // Assert
        result.IsSuccess.Should().BeTrue();
        _fileManagerMock.Verify(f => f.WritePageAsync(It.Is<Page>(p => p.PageId == pageId)), Times.Once);
    }

    [Fact]
    public async Task FlushPageAsync_PageNotExists_ShouldReturnSuccess()
    {
        // Arrange
        const ulong pageId = 999;

        // Act
        var result = await _bufferManager.FlushPageAsync(pageId);

        // Assert
        result.IsSuccess.Should().BeTrue();
        _fileManagerMock.Verify(f => f.WritePageAsync(It.IsAny<Page>()), Times.Never);
    }

    [Fact]
    public async Task FlushPageAsync_FileManagerError_ShouldReturnError()
    {
        // Arrange
        const ulong pageId = 1;
        var page = CreateTestPage(pageId);
        var fileError = new StoreError("Write failed");

        _fileManagerMock.Setup(f => f.WritePageAsync(page))
            .ReturnsAsync(Result<YggdrasilVinum.Models.Unit, StoreError>.Error(fileError));

        await _bufferManager.PutPageAsync(page);

        // Act
        var result = await _bufferManager.FlushPageAsync(pageId);

        // Assert
        result.IsError.Should().BeTrue();
        var error = result.GetErrorOrThrow();
        error.Message.Should().Contain("Failed to write page");
    }

    [Fact]
    public async Task SetPageDirty_PageExists_ShouldSetDirtyFlag()
    {
        // Arrange
        const ulong pageId = 1;
        var page = CreateTestPage(pageId);
        await _bufferManager.PutPageAsync(page);

        // Verify page is not dirty initially
        page.IsDirty.Should().BeFalse();

        // Act
        var result = await _bufferManager.SetPageDirty(pageId);

        // Assert
        result.IsSuccess.Should().BeTrue();
        page.IsDirty.Should().BeTrue();
    }

    [Fact]
    public async Task SetPageDirty_PageNotExists_ShouldReturnSuccess()
    {
        // Arrange
        const ulong pageId = 999;

        // Act
        var result = await _bufferManager.SetPageDirty(pageId);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task FlushAllFramesAsync_ShouldFlushAllPages()
    {
        // Arrange
        var page1 = CreateTestPage(1);
        var page2 = CreateTestPage(2);

        _fileManagerMock.Setup(f => f.WritePageAsync(It.IsAny<Page>()))
            .ReturnsAsync(Result<YggdrasilVinum.Models.Unit, StoreError>.Success(YggdrasilVinum.Models.Unit.Value));

        await _bufferManager.PutPageAsync(page1);
        await _bufferManager.PutPageAsync(page2);

        // Act
        var result = await _bufferManager.FlushAllFramesAsync();

        // Assert
        result.IsSuccess.Should().BeTrue();
        _fileManagerMock.Verify(f => f.WritePageAsync(It.Is<Page>(p => p.PageId == 1)), Times.Once);
        _fileManagerMock.Verify(f => f.WritePageAsync(It.Is<Page>(p => p.PageId == 2)), Times.Once);
    }

    [Fact]
    public async Task FlushAllFramesAsync_FileManagerError_ShouldReturnError()
    {
        // Arrange
        var page1 = CreateTestPage(1);
        var fileError = new StoreError("Write failed");

        _fileManagerMock.Setup(f => f.WritePageAsync(page1))
            .ReturnsAsync(Result<YggdrasilVinum.Models.Unit, StoreError>.Error(fileError));

        await _bufferManager.PutPageAsync(page1);

        // Act
        var result = await _bufferManager.FlushAllFramesAsync();

        // Assert
        result.IsError.Should().BeTrue();
        var error = result.GetErrorOrThrow();
        error.Message.Should().Contain("Failed to flush page");
    }

    [Fact]
    public async Task BufferManager_Integration_ShouldHandleComplexScenario()
    {
        // Arrange - Complex scenario with multiple operations
        var pages = new[] { CreateTestPage(1), CreateTestPage(2), CreateTestPage(3), CreateTestPage(4) };

        // Setup file manager for all operations
        foreach (var page in pages)
        {
            _fileManagerMock.Setup(f => f.PageExistsAsync(page.PageId))
                .ReturnsAsync(Result<bool, StoreError>.Success(true));
            _fileManagerMock.Setup(f => f.ReadPageAsync(page.PageId))
                .ReturnsAsync(Result<Page, StoreError>.Success(page));
            _fileManagerMock.Setup(f => f.WritePageAsync(It.Is<Page>(p => p.PageId == page.PageId)))
                .ReturnsAsync(Result<YggdrasilVinum.Models.Unit, StoreError>.Success(YggdrasilVinum.Models.Unit.Value));
        }

        // Act - Perform complex sequence of operations
        // 1. Load pages 1 and 2 (fills buffer)
        await _bufferManager.LoadPageAsync(1);
        await _bufferManager.LoadPageAsync(2);

        // 2. Set page 1 as dirty
        await _bufferManager.SetPageDirty(1);

        // 3. Access page 1 again (moves to front of LRU)
        await _bufferManager.LoadPageAsync(1);

        // 4. Load page 3 (should evict page 2)
        await _bufferManager.LoadPageAsync(3);

        // 5. Put page 4 (should evict page 1, which is dirty)
        await _bufferManager.PutPageAsync(pages[3]);

        // 6. Flush all remaining pages
        await _bufferManager.FlushAllFramesAsync();

        // Assert
        // Page 2 should be evicted when loading page 3
        _fileManagerMock.Verify(f => f.WritePageAsync(It.Is<Page>(p => p.PageId == 2)), Times.Once);

        // Page 1 should be evicted when putting page 4 (and it was dirty)
        _fileManagerMock.Verify(f => f.WritePageAsync(It.Is<Page>(p => p.PageId == 1)), Times.Once);

        // Pages 3 and 4 should be flushed during FlushAllFramesAsync
        _fileManagerMock.Verify(f => f.WritePageAsync(It.Is<Page>(p => p.PageId == 3)), Times.Once);
        _fileManagerMock.Verify(f => f.WritePageAsync(It.Is<Page>(p => p.PageId == 4)), Times.Once);
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 0)]
    [InlineData(0, 0)]
    public void LruBufferManager_ShouldNotBeCreatedWithBadParameters(ulong amountOfPageFrames,
        ulong amountOfIndexFrames)
    {
        // AAA
        Assert.Throws<ArgumentOutOfRangeException>(() => new LruBufferManager(
            _fileManagerMock.Object, amountOfPageFrames, amountOfIndexFrames
        ));
    }
}
