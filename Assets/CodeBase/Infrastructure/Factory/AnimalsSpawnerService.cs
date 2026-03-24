using CodeBase.Animals;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
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
            _ = SpawnAnimalsLoop();
        }

        private async UniTaskVoid SpawnAnimalsLoop()
        {
            while (true)
            {
                //Vector2 spawnAt = GetRandomSpawnPosition();

                if (Random.value < 0.5f)
                    await _gameFactory.CreateAnimal<Frog>(Vector2.zero);
                else
                    await _gameFactory.CreateAnimal<Snake>(Vector2.zero);

                await UniTask.Delay(2000);
            }
        }

        private static Vector2 GetRandomSpawnPosition()
        {
            float x = Random.Range(-8f, 8f);
            float y = Random.Range(-4f, 4f);
            return new Vector2(x, y);
        }
    }
}
