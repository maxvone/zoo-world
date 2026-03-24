using CodeBase.AssetManagement;
using Cysharp.Threading.Tasks;

namespace CodeBase.Services
{
    public class StaticDataService : IStaticDataService
    {
        private readonly IAssetProvider _assets;

        public StaticDataService(IAssetProvider assetProvider)
        {
           _assets = assetProvider; 
        }

        public async UniTask<T> GetData<T>(string key) where T : class
        {
            T staticData = await _assets.Load<T>(key);

            if (staticData == null)
            {
                throw new System.Exception($"Can't load static data by key {key}");
            }

            return staticData;
        }
    }
}