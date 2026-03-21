using Cysharp.Threading.Tasks;

namespace CodeBase.Services
{
    public interface ISceneLoaderService : IService
    {
        void Load(string sceneName);
		UniTask LoadMainMenuSceneAsync(string sceneName);
    }
}