# 1.0.0 (2025-05-16)


### Bug Fixes

* **build-push-container:** docker container not being created ([b6642e2](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/b6642e2db52b95a9d8132ce602099b0a13a22ec0))
* **build-push-container:** have write permissions ([c38e63a](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/c38e63af0277d0250296fe8e51fc61ad60e28e35))
* change argument to use option ([1ce2962](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/1ce29629e32db666ea8c205d3bd5e047d754f103))
* **LRUBufferManager:** check if there is a page on the file manager ([675ec9d](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/675ec9d14ac982bfebb756b29b994a74da49ae40))
* **Program:** get tree height properly ([4c623c2](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/4c623c2ada7aa8f111e3b3c563552d39f50e30f4))
* **releasrec:** change branch to master ([f8bfd9f](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/f8bfd9fa7ceddde7714550430116964374845adc))
* replace console to log ([e373388](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/e37338849603384439344a56bbf7639f8ef839bb))
* **SequentialHeapFileManager:** flush on dispose ([7d1316a](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/7d1316aa297746d019f3f3dd64c25bcf6244940f))


### Features

* add application factory ([801fd7d](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/801fd7d0b5d17758c899c3f3f235cf8222d3e942))
* add base buffer manager ([f5d6464](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/f5d64646e67d2efa5d7a309edfed6000a6ca5dfc))
* add base unit tests ([bf0a9a8](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/bf0a9a880ddd83cc58179f22b781e0584fb0c9e9))
* add command line lib ([75ae3a7](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/75ae3a75288c8e13dcee34ef3ccf4f984af7e200))
* add command line parsing ([17f1c8f](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/17f1c8f5616b5f234b2775663d34191adb205dad))
* add command processor ([5a265f6](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/5a265f6b35b23424ea0be90cb8757f4c0afb019f))
* add Database class ([7f6da04](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/7f6da04cf692446d0bcc45ff13f59237abc72713))
* add Dockerfile ([0e16256](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/0e1625630351fd5856df17351be1a1d52acbfb41))
* add EqualitySearchProcessor class ([4b86667](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/4b866677571ed3dee2c55c6989300872a8d551d9))
* add fluentassertions and moq ([c05e597](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/c05e597e61e9875e5755788223101811edab17c8))
* add HarvestYearSearchProcessor ([967c8c8](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/967c8c875d77af9680b9b71259bbd8ecd49126af))
* add IFileManager ([29d0737](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/29d0737cf0f93f9c8cafa73bacdab4aa5023422d))
* add initial commit ([a9d8897](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/a9d88977c2a191e2f0fcc61ff7d16fd7c8a14a3e))
* add insertion to file on program ([d2d640f](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/d2d640f821e424c84cd8aecd331314a102185446))
* add InsertProcessor class ([bef5d49](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/bef5d49a33fc633d7ca12b992d8a90288eb05339))
* add LRU Buffer Manager ([05e8510](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/05e8510be7f8323b122104ecfc31853c74f7afd8))
* add page model ([a592bdb](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/a592bdb7cd73bd4963d6e560a7be9b13548078f4))
* add result tests ([5a7218d](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/5a7218d6d07b8622b0c04b68eb7a336eca33baaf))
* add sequential heap file manager ([4856341](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/4856341370057f904142090192c1d314fc665b4f))
* add serilog for structured logging ([386a911](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/386a911caa76216ba384c066c49018191d961995))
* add specs ([46dedae](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/46dedae1610bdaf5d412dae99015fbfcdbc142c1))
* add WineProcessor ([68288d8](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/68288d865beee712761ad896761331f7d7667cd0))
* add xunit and move original project to new shit ([8b33a3e](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/8b33a3ec0be7f0bf4138a4862ce92c275ad26ad9))
* **ApplicationFactory:** add CreateFileManager and BufferManager ([3a1670d](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/3a1670dd86d92106dc4fe5155d993a179b8dbd0b))
* **index:** integrate B+ tree with InsertProcessor ([4c1723f](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/4c1723ff15130f42cbdddc44190516e042b52261))
* **models:** add file-backed B+ tree implementation ([fad8c35](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/fad8c354a497074ec2db0206eb61087968a0d5fb))
* **models:** add generic B+ tree implementation ([669aea7](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/669aea7ec1dca859b84d832b9106b65eb4e3279e))
* process wine csv ([2c0cf0e](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/2c0cf0ea01be63955f70ff5d0532b500fc4eb0f8))
* **Program:** add buffer, file and processors on main ([831d427](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/831d427e332c8f555930a125ab3776fa83522cf1))
* **SequentialHeapFileManager:** add check if page has enough space ([219f70c](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/219f70c97f495f78bea74a44f5c5f852b249bf2a))
* **SequentialHeapFileManager:** create the first page if there is no initial heap ([190a48a](https://github.com/ArtroxGabriel/YggdrasilVinum/commit/190a48af33a9ba15b02d7844b0e9bd3566fc558f))

# [1.2.0](https://github.com/talDoFlemis/YggdrasilVinum/compare/v1.1.0...v1.2.0) (2025-05-16)


### Bug Fixes

* change argument to use option ([1ce2962](https://github.com/talDoFlemis/YggdrasilVinum/commit/1ce29629e32db666ea8c205d3bd5e047d754f103))
* **Program:** get tree height properly ([4c623c2](https://github.com/talDoFlemis/YggdrasilVinum/commit/4c623c2ada7aa8f111e3b3c563552d39f50e30f4))


### Features

* add HarvestYearSearchProcessor ([967c8c8](https://github.com/talDoFlemis/YggdrasilVinum/commit/967c8c875d77af9680b9b71259bbd8ecd49126af))
* add insertion to file on program ([d2d640f](https://github.com/talDoFlemis/YggdrasilVinum/commit/d2d640f821e424c84cd8aecd331314a102185446))
* add WineProcessor ([68288d8](https://github.com/talDoFlemis/YggdrasilVinum/commit/68288d865beee712761ad896761331f7d7667cd0))
* process wine csv ([2c0cf0e](https://github.com/talDoFlemis/YggdrasilVinum/commit/2c0cf0ea01be63955f70ff5d0532b500fc4eb0f8))

# [1.1.0](https://github.com/talDoFlemis/YggdrasilVinum/compare/v1.0.1...v1.1.0) (2025-05-15)


### Features

* **index:** integrate B+ tree with InsertProcessor ([4c1723f](https://github.com/talDoFlemis/YggdrasilVinum/commit/4c1723ff15130f42cbdddc44190516e042b52261))
* **models:** add file-backed B+ tree implementation ([fad8c35](https://github.com/talDoFlemis/YggdrasilVinum/commit/fad8c354a497074ec2db0206eb61087968a0d5fb))

## [1.0.1](https://github.com/talDoFlemis/YggdrasilVinum/compare/v1.0.0...v1.0.1) (2025-05-15)


### Bug Fixes

* **build-push-container:** docker container not being created ([b6642e2](https://github.com/talDoFlemis/YggdrasilVinum/commit/b6642e2db52b95a9d8132ce602099b0a13a22ec0))

# 1.0.0 (2025-05-15)


### Bug Fixes

* **build-push-container:** have write permissions ([c38e63a](https://github.com/talDoFlemis/YggdrasilVinum/commit/c38e63af0277d0250296fe8e51fc61ad60e28e35))
* **LRUBufferManager:** check if there is a page on the file manager ([675ec9d](https://github.com/talDoFlemis/YggdrasilVinum/commit/675ec9d14ac982bfebb756b29b994a74da49ae40))
* **releasrec:** change branch to master ([f8bfd9f](https://github.com/talDoFlemis/YggdrasilVinum/commit/f8bfd9fa7ceddde7714550430116964374845adc))
* replace console to log ([e373388](https://github.com/talDoFlemis/YggdrasilVinum/commit/e37338849603384439344a56bbf7639f8ef839bb))
* **SequentialHeapFileManager:** flush on dispose ([7d1316a](https://github.com/talDoFlemis/YggdrasilVinum/commit/7d1316aa297746d019f3f3dd64c25bcf6244940f))


### Features

* add application factory ([801fd7d](https://github.com/talDoFlemis/YggdrasilVinum/commit/801fd7d0b5d17758c899c3f3f235cf8222d3e942))
* add base buffer manager ([f5d6464](https://github.com/talDoFlemis/YggdrasilVinum/commit/f5d64646e67d2efa5d7a309edfed6000a6ca5dfc))
* add base unit tests ([bf0a9a8](https://github.com/talDoFlemis/YggdrasilVinum/commit/bf0a9a880ddd83cc58179f22b781e0584fb0c9e9))
* add command line lib ([75ae3a7](https://github.com/talDoFlemis/YggdrasilVinum/commit/75ae3a75288c8e13dcee34ef3ccf4f984af7e200))
* add command line parsing ([17f1c8f](https://github.com/talDoFlemis/YggdrasilVinum/commit/17f1c8f5616b5f234b2775663d34191adb205dad))
* add command processor ([5a265f6](https://github.com/talDoFlemis/YggdrasilVinum/commit/5a265f6b35b23424ea0be90cb8757f4c0afb019f))
* add Database class ([7f6da04](https://github.com/talDoFlemis/YggdrasilVinum/commit/7f6da04cf692446d0bcc45ff13f59237abc72713))
* add Dockerfile ([0e16256](https://github.com/talDoFlemis/YggdrasilVinum/commit/0e1625630351fd5856df17351be1a1d52acbfb41))
* add EqualitySearchProcessor class ([4b86667](https://github.com/talDoFlemis/YggdrasilVinum/commit/4b866677571ed3dee2c55c6989300872a8d551d9))
* add fluentassertions and moq ([c05e597](https://github.com/talDoFlemis/YggdrasilVinum/commit/c05e597e61e9875e5755788223101811edab17c8))
* add IFileManager ([29d0737](https://github.com/talDoFlemis/YggdrasilVinum/commit/29d0737cf0f93f9c8cafa73bacdab4aa5023422d))
* add initial commit ([a9d8897](https://github.com/talDoFlemis/YggdrasilVinum/commit/a9d88977c2a191e2f0fcc61ff7d16fd7c8a14a3e))
* add InsertProcessor class ([bef5d49](https://github.com/talDoFlemis/YggdrasilVinum/commit/bef5d49a33fc633d7ca12b992d8a90288eb05339))
* add LRU Buffer Manager ([05e8510](https://github.com/talDoFlemis/YggdrasilVinum/commit/05e8510be7f8323b122104ecfc31853c74f7afd8))
* add page model ([a592bdb](https://github.com/talDoFlemis/YggdrasilVinum/commit/a592bdb7cd73bd4963d6e560a7be9b13548078f4))
* add result tests ([5a7218d](https://github.com/talDoFlemis/YggdrasilVinum/commit/5a7218d6d07b8622b0c04b68eb7a336eca33baaf))
* add sequential heap file manager ([4856341](https://github.com/talDoFlemis/YggdrasilVinum/commit/4856341370057f904142090192c1d314fc665b4f))
* add serilog for structured logging ([386a911](https://github.com/talDoFlemis/YggdrasilVinum/commit/386a911caa76216ba384c066c49018191d961995))
* add specs ([46dedae](https://github.com/talDoFlemis/YggdrasilVinum/commit/46dedae1610bdaf5d412dae99015fbfcdbc142c1))
* add xunit and move original project to new shit ([8b33a3e](https://github.com/talDoFlemis/YggdrasilVinum/commit/8b33a3ec0be7f0bf4138a4862ce92c275ad26ad9))
* **ApplicationFactory:** add CreateFileManager and BufferManager ([3a1670d](https://github.com/talDoFlemis/YggdrasilVinum/commit/3a1670dd86d92106dc4fe5155d993a179b8dbd0b))
* **models:** add generic B+ tree implementation ([669aea7](https://github.com/talDoFlemis/YggdrasilVinum/commit/669aea7ec1dca859b84d832b9106b65eb4e3279e))
* **Program:** add buffer, file and processors on main ([831d427](https://github.com/talDoFlemis/YggdrasilVinum/commit/831d427e332c8f555930a125ab3776fa83522cf1))
* **SequentialHeapFileManager:** add check if page has enough space ([219f70c](https://github.com/talDoFlemis/YggdrasilVinum/commit/219f70c97f495f78bea74a44f5c5f852b249bf2a))
* **SequentialHeapFileManager:** create the first page if there is no initial heap ([190a48a](https://github.com/talDoFlemis/YggdrasilVinum/commit/190a48af33a9ba15b02d7844b0e9bd3566fc558f))
