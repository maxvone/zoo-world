using System.Threading.Tasks;
using CodeBase.AssetManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
	public class GameFactory : IGameFactory
	{
		private readonly IAssetProvider _assets;

		public GameFactory(IAssetProvider assets)
		{
			_assets = assets;
		}

		public async UniTask WarmUp()
		{
			await _assets.Load<GameObject>(AssetAddress.Level1Path);
			//await _assets.Load<GameObject>(AssetAddress.HudPath);

			//GameObject enemyPrefab = await _assets.Load<GameObject>(AssetAddress.EnemyPath);
			//_enemySpawner.Construct(enemyPrefab);

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

		//public async Task<GameObject> CreateHero(Vector2 at)
		//{
		//    HeroInstance = await InstantiateAsync(AssetAddress.HeroPath, at);
		//    HeroInstance.GetComponent<HeroMove>().Construct(_inputService);
		//    HeroInstance.GetComponent<HeroAttack>().Construct(_inputService, this);

		//    return HeroInstance;
		//}

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
			GameObject gameObject = await _assets.Instantiate(path: prefabPath, at: at);
			return gameObject;
		}
    }
}