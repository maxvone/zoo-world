using CodeBase.AssetManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
  public class UiFactory : IUiFactory
  {
    private readonly IAssetProvider _assetProvider;

    public Transform UiRoot { get; private set; }

    public UiFactory(IAssetProvider assetProvider)
    {
      _assetProvider = assetProvider;
    }

    public async UniTask CreateUIRoot()
    {
      GameObject root = await _assetProvider.Instantiate(AssetAddress.UiRootPath);
      UiRoot = root.transform;
    }
  }
}