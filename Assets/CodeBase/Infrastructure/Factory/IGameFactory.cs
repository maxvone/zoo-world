using System.Threading.Tasks;
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
		UniTask<GameObject> CreateFrog(Vector2 at);
		UniTask<GameObject> CreateSnake(Vector2 at);
    }
}