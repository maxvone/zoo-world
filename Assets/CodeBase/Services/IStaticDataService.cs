using Cysharp.Threading.Tasks;

namespace CodeBase.Services
{
    public interface IStaticDataService : IService
    {
        UniTask<T> GetData<T>(string key) where T : class;
    }
}