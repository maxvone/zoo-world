using CodeBase.Infrastructure.Services;

namespace CodeBase.Infrastructure.States
{
    public class GameLoopState : IState
	{
        private readonly GameStateMachine _stateMachine;
        private IAnimalsSpawnerService _animalsSpawnerService;

        public GameLoopState(GameStateMachine gameStateMachine, IAnimalsSpawnerService animalsSpawnerService)
		{
			_stateMachine = gameStateMachine;
			_animalsSpawnerService = animalsSpawnerService;
		}

		public void Enter()
		{
			_animalsSpawnerService.StartOngoingSpawning();
		}

		public void Exit() { }
	}
}