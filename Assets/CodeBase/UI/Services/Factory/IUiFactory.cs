using System.Threading.Tasks;
using CodeBase.Services;
using CodeBase.UI.Services.Overlays;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.UI.Services.Factory
{
    public interface IUiFactory : IService
    {
        public Transform UiRoot { get; }
        public Hud Hud { get; }
        UniTask CreateHud();
        UniTask CreateUIRoot();
    }
}