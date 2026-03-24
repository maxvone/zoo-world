using CodeBase.AssetManagement;
using CodeBase.Infrastructure.Factory;
using CodeBase.Infrastructure.Services;
using CodeBase.Services;
using CodeBase.UI.Services.Factory;
using UnityEngine;

namespace CodeBase.Infrastructure.States
{
    public class BootstrapState : IPayloadedState<Canvas>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine stateMachine, AllServices services)
        {
            _stateMachine = stateMachine;
            _services = services;

            RegisterServices();
        }

        public void Enter(Canvas loadingScreen) =>
          EnterLoadLevel(loadingScreen);

        private void EnterLoadLevel(Canvas loadingScreen) =>
          _stateMachine.Enter<LoadLevelState, Canvas>(loadingScreen);

        public void Exit() { }

        private void RegisterServices()
        {
            RegisterAssetProvider();
            _services.RegisterSingle<IStaticDataService>(new StaticDataService(_services.Single<IAssetProvider>()));
            _services.RegisterSingle<ISceneLoaderService>(new SceneLoaderService());
            _services.RegisterSingle<IUiFactory>(new UiFactory(_services.Single<IAssetProvider>()));
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services));
            _services.RegisterSingle<IBoundsReturnService>(new BoundsReturnService());
            _services.RegisterSingle<IDeathResolverService>(new DeathResolverService(_services.Single<IUiFactory>()));
            _services.RegisterSingle<IAnimalsSpawnerService>(new AnimalsSpawnerService(_services.Single<IGameFactory>()));
        }

        private void RegisterAssetProvider()
        {
            AssetProvider assetProvider = new();
            _services.RegisterSingle<IAssetProvider>(assetProvider);
            assetProvider.Initialize();
        }
    }
}