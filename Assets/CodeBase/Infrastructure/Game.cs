using CodeBase.Infrastructure.States;
using CodeBase.Services;

namespace CodeBase.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;

        public Game()
        {
            StateMachine = new GameStateMachine(AllServices.Container);
        }
    }
}