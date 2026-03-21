namespace CodeBase.Infrastructure.States
{
    public interface IState : IExitableState
    {
        void Enter();
    }

    public interface IPayloadedState<TPayload> : IExitableState
    {
        void Enter(TPayload hero);
    }

    public interface IExitableState
    {
        void Exit();
    }
}