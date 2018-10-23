import { Core\generatorCrudModule } from './core\generator-crud.module';

describe('Core\generatorCrudModule', () => {
  let core\generatorCrudModule: Core\generatorCrudModule;

  beforeEach(() => {
    core\generatorCrudModule = new Core\generatorCrudModule();
  });

  it('should create an instance', () => {
    expect(core\generatorCrudModule).toBeTruthy();
  });
});
