using CodeBase.AssetManagement;
using CodeBase.Services;
using CodeBase.UI.Services.Factory;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, AllServices services)
        {
            _stateMachine = stateMachine;
            _services = services;

            RegisterServices();
        }

        public void Enter() =>
          EnterLoadLevel();

        private void EnterLoadLevel() =>
          _stateMachine.Enter<LoadLevelState>();

        public void Exit() { }

        private void RegisterServices()
        {
            RegisterAssetProvider();
            _services.RegisterSingle<ISceneLoaderService>(new SceneLoaderService());
            _services.RegisterSingle<IUiFactory>(new UiFactory(_services.Single<IAssetProvider>()));
        }

        private void RegisterAssetProvider()
        {
            AssetProvider assetProvider = new();
            _services.RegisterSingle<IAssetProvider>(assetProvider);
            assetProvider.Initialize();
        }
    }
}