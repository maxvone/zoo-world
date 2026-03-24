using CodeBase.Animals;
using CodeBase.Infrastructure.Factory;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Services
{
    public class AnimalsSpawnerService : IAnimalsSpawnerService
    {
        private readonly IGameFactory _gameFactory;

        public AnimalsSpawnerService(IGameFactory gameFactory)
        {
            _gameFactory = gameFactory;
        }

        public void StartOngoingSpawning()
        {
            SpawnAnimalsLoop().Forget();
        }

        private async UniTaskVoid SpawnAnimalsLoop()
        {
            while (true)
            {
                if (Random.value < 0.5f)
                    await _gameFactory.CreateAnimal<Frog>(Vector2.zero);
                else
                    await _gameFactory.CreateAnimal<Snake>(Vector2.zero);

                await UniTask.Delay(2000);
            }
        }
    }
}
