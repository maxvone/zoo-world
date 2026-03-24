using CodeBase.Animals;
using CodeBase.AssetManagement;
using CodeBase.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
	public class GameFactory : IGameFactory
	{
        private readonly AllServices _allServices;
        private readonly AnimalsPool _animalSpawner;

        public GameFactory(AllServices allServices)
		{
			_allServices = allServices;

			_animalSpawner = new AnimalsPool(allServices);
		}

		public async UniTask WarmUp()
		{
			await _allServices.Single<IAssetProvider>().Load<GameObject>(AssetAddress.Level1Path);

			GameObject frogPrefab = await _allServices.Single<IAssetProvider>().Load<GameObject>(AssetAddress.FrogPath);
			GameObject snakePrefab = await _allServices.Single<IAssetProvider>().Load<GameObject>(AssetAddress.SnakePath);

			_animalSpawner.RegisterPrefab<Frog>(frogPrefab);
			_animalSpawner.RegisterPrefab<Snake>(snakePrefab);
		}

		public async UniTask<GameObject> CreateLevelAsync()
		{
			GameObject levelInstance = await InstantiateAsync(AssetAddress.Level1Path, Vector2.zero);

			return levelInstance;
		}

        public async UniTask<GameObject> CreateAnimal<T>(Vector2 at) where T : AnimalBase =>
			_animalSpawner.Get<T>(at).gameObject;

        public async UniTask<GameObject> InstantiateAsync(string prefabPath, Vector2 at)
		{
			GameObject gameObject = await _allServices.Single<IAssetProvider>().Instantiate(path: prefabPath, at: at);
			return gameObject;
		}
    }
}