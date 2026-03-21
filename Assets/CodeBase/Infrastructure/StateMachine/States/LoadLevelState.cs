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

        public LoadLevelState(GameStateMachine gameStateMachine,
			ISceneLoaderService sceneLoaderService, IUiFactory uiFactory)
		{
			_stateMachine = gameStateMachine;
            _sceneLoaderService = sceneLoaderService;
            _uiFactory = uiFactory;
		}

		public async void Enter()
		{
			UniTask loadingMainMenuOperation = _sceneLoaderService
				.LoadMainMenuSceneAsync(LevelSceneName);
			await UniTask.WaitUntil(() => loadingMainMenuOperation.Status == UniTaskStatus.Succeeded);

			await InitUIRoot();

			_stateMachine.Enter<GameLoopState>();
		}

		public void Exit() { }

		private async UniTask InitUIRoot() =>
		  await _uiFactory.CreateUIRoot();

	}
}