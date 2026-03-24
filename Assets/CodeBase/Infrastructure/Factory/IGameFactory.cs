using System.Threading.Tasks;
using CodeBase.Animals;
using CodeBase.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        UniTask WarmUp();
        UniTask<GameObject> InstantiateAsync(string prefabPath, Vector2 at);
		UniTask<GameObject> CreateLevelAsync();
        UniTask<GameObject> CreateAnimal<T>(Vector2 at) where T : AnimalBase;
    }
}