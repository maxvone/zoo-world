using CodeBase.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.AssetManagement
{
  public interface IAssetProvider : IService
  {
    void Initialize();
    UniTask<T> Load<T>(string address) where T : class;
    UniTask<GameObject> Instantiate(string path, Vector3 at);
    UniTask<GameObject> Instantiate(string path);
    void Cleanup();
  }
}