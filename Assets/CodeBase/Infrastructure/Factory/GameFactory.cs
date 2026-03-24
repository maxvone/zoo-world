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

		public GameFactory(AllServices allServices)
		{
			_allServices = allServices;
		}

		public async UniTask WarmUp()
		{
			await _allServices.Single<IAssetProvider>().Load<GameObject>(AssetAddress.Level1Path);

			GameObject frogPrefab = await _allServices.Single<IAssetProvider>().Load<GameObject>(AssetAddress.FrogPath);
			GameObject snakePrefab = await _allServices.Single<IAssetProvider>().Load<GameObject>(AssetAddress.SnakePath);

			//_enemySpawner.Construct(frogPrefab);

			//GameObject bulletPrefab = await _assets.Load<GameObject>(AssetAddress.HeroBulletPath);
			//_heroBulletLauncher.Construct(bulletPrefab);

			//GameObject enemyBulletPrefab = await _assets.Load<GameObject>(AssetAddress.EnemyBulletPath);
			//_enemyBulletLauncher.Construct(enemyBulletPrefab);

			//GameObject explosionPrefab = await _assets.Load<GameObject>(AssetAddress.ExplosionPath);
			//_explosionsSpawner.Construct(explosionPrefab);
		}

		public async UniTask<GameObject> CreateLevelAsync()
		{
			GameObject levelInstance = await InstantiateAsync(AssetAddress.Level1Path, Vector2.zero);

			return levelInstance;
		}

		public async UniTask<GameObject> CreateFrog(Vector2 at)
		{
		    GameObject instance = await InstantiateAsync(AssetAddress.FrogPath, at);
		    instance.GetComponent<Frog>().Construct(_allServices.Single<IBoundsReturnService>(), _allServices.Single<IDeathResolverService>());

		    return instance;
		}

		public async UniTask<GameObject> CreateSnake(Vector2 at)
		{
		    GameObject instance = await InstantiateAsync(AssetAddress.SnakePath, at);
		    instance.GetComponent<Snake>().Construct(_allServices.Single<IBoundsReturnService>(), _allServices.Single<IDeathResolverService>());

		    return instance;
		}

		//public GameObject CreateEnemy(Vector2 at)
		//{
		//    GameObject enemy = _enemySpawner.Get(at);
		//    enemy.GetComponent<EnemyDeath>().Construct(this, _explosionsSpawner, _scoreCounter);
		//    enemy.GetComponent<EnemyAttack>().Construct(this);

		//    return enemy;
		//}

		//public HeroBullet CreateHeroBullet(Vector2 at) =>
		//    _heroBulletLauncher.Get(at);

		//public GameObject CreateExplosion(Vector2 at) =>
		//    _explosionsSpawner.Get(at);

		//public EnemyBullet CreateEnemyBullet(Vector3 at)
		//{
		//    EnemyBullet bullet = _enemyBulletLauncher.Get(at);
		//    bullet.Construct(HeroInstance.transform, _enemyBulletLauncher, this, _explosionsSpawner);
		//    return bullet;
		//}

		//public async Task<GameObject> CreateHud()
		//{
		//    GameObject hud = await InstantiateAsync(AssetAddress.HudPath, Vector2.zero);
		//    return hud;
		//}


		public async UniTask<GameObject> InstantiateAsync(string prefabPath, Vector2 at)
		{
			GameObject gameObject = await _allServices.Single<IAssetProvider>().Instantiate(path: prefabPath, at: at);
			return gameObject;
		}
    }
}