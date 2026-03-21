using CodeBase.Infrastructure.States;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        [SerializeField] private Canvas _loadingScreen;
        private Game _game;
        

        private void Awake()
        {
            _game = new Game();
            _game.StateMachine.Enter<BootstrapState, Canvas>(_loadingScreen);

            DontDestroyOnLoad(this);
        }
    }
}