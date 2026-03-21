namespace CodeBase.Infrastructure.States
{
    public class MainMenuLoopState : IState
    {
        private readonly GameStateMachine _stateMachine;

        public MainMenuLoopState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
        }

        public void Exit()
        {
        }
    }
}