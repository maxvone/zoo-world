using System;
using System.Threading.Tasks;
using CodeBase.Infrastructure.Factory;
using CodeBase.Services;
using CodeBase.UI.Services.Factory;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.States
{
	public class LoadLevelState : IState
	{
		private const string LevelSceneName = "LevelScene"; //TODO: Move to config

		private readonly GameStateMachine _stateMachine;
		private readonly ISceneLoaderService _sceneLoaderService;
        private readonly IUiFactory _uiFactory;
        private readonly IGameFactory _gameFactory;


        public LoadLevelState(GameStateMachine gameStateMachine,
			ISceneLoaderService sceneLoaderService, IUiFactory uiFactory, IGameFactory gameFactory)
		{
			_stateMachine = gameStateMachine;
            _sceneLoaderService = sceneLoaderService;
            _uiFactory = uiFactory;
            _gameFactory = gameFactory;
		}

		public async void Enter()
		{
			UniTask loadingMainMenuOperation = _sceneLoaderService
				.LoadMainMenuSceneAsync(LevelSceneName);
			await UniTask.WaitUntil(() => loadingMainMenuOperation.Status == UniTaskStatus.Succeeded);

			await _gameFactory.WarmUp();
			await InitUIRoot();
			await InitLevel();

			_stateMachine.Enter<GameLoopState>();
		}

        private async UniTask InitLevel()
		{
			await _gameFactory.CreateLevelAsync();
		}

        public void Exit() { }

		private async UniTask InitUIRoot() =>
		  await _uiFactory.CreateUIRoot();

	}
}