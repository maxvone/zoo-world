using CodeBase.AssetManagement;
using CodeBase.UI.Services.Overlays;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
	public class UiFactory : IUiFactory
	{
		private readonly IAssetProvider _assetProvider;

		public Transform UiRoot { get; private set; }
		public Hud Hud { get; private set; }

		public UiFactory(IAssetProvider assetProvider)
		{
			_assetProvider = assetProvider;
		}

		public async UniTask CreateUIRoot()
		{
			GameObject root = await _assetProvider.Instantiate(AssetAddress.UiRootPath);
			UiRoot = root.transform;
		}

		public async UniTask CreateHud()
		{
			GameObject hud = await _assetProvider.Instantiate(AssetAddress.HudPath);
			Hud = hud.GetComponent<Hud>();
		}
	}
}