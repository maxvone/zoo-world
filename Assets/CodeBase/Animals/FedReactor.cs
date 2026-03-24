using CodeBase.AssetManagement;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace CodeBase.Animals
{
    [RequireComponent(typeof(AnimalBase))]
    public class FedReactor : MonoBehaviour
    {
        [SerializeField] private AnimalBase _animalBase;

        private IAssetProvider _assetProvider;

        public void Construct(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        private void OnEnable() =>
            _animalBase.Fed += ReactToFeeding;

        private void OnDisable() =>
            _animalBase.Fed -= ReactToFeeding;

        private void ReactToFeeding() =>
            PlayTastyLabel().Forget();

        private async UniTask PlayTastyLabel()
        {
            var labelPrefab = await _assetProvider.Load<GameObject>(AssetAddress.TastyLabelPath);
            var labelInstance = Instantiate(labelPrefab, transform.position + Vector3.back * 1.1f, Quaternion.identity);
            labelInstance.transform.rotation = Quaternion.Euler(90, 0, 0);
            labelInstance.transform.SetParent(transform, true);

            Destroy(labelInstance, 2f);
        }
    }
}