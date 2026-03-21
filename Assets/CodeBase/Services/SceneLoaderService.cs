using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase.Services
{
	public class SceneLoaderService : ISceneLoaderService
	{
		public void Load(string sceneName) =>
          SceneManager.LoadScene(sceneName);

		public async UniTask LoadMainMenuSceneAsync(string sceneName)
		{
			AsyncOperation loadingSceneOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

			await loadingSceneOperation;

			Scene scene = SceneManager.GetSceneByName(sceneName);
			if (scene.IsValid())
				SceneManager.SetActiveScene(scene);
		}
	}
}