using CodeBase.Animals;
using CodeBase.Services;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace CodeBase.Infrastructure.Factory
{

    public class AnimalsPool
    {
        private readonly AllServices _allServices;
        private Dictionary<Type, IObjectPool<AnimalBase>> _animalPools;
        private Dictionary<Type, GameObject> _animalPrefabs;


        public AnimalsPool(AllServices allServices)
        {
            _allServices = allServices;
            _animalPools = new Dictionary<Type, IObjectPool<AnimalBase>>();
            _animalPrefabs = new Dictionary<Type, GameObject>();
        }

        public void RegisterPrefab<T>(GameObject animalPrefab) where T : AnimalBase
        {
            Type animalType = typeof(T);
            
            if (_animalPrefabs.ContainsKey(animalType))
            {
                Debug.LogWarning($"Prefab for type '{animalType.Name}' is already registered.");
                return;
            }

            _animalPrefabs[animalType] = animalPrefab;

            // Create a pool for this prefab type
            _animalPools[animalType] = new ObjectPool<AnimalBase>(
                () => CreateAnimal(animalType),
                OnGetAnimal,
                OnReleaseAnimal,
                OnDestroyAnimal
            );
        }

        public AnimalBase Get<T>(Vector2 at) where T : AnimalBase
        {
            Type animalType = typeof(T);
            
            if (!_animalPools.ContainsKey(animalType))
            {
                Debug.LogError($"No pool found for type '{animalType.Name}'. Make sure to RegisterPrefab first.");
                return null;
            }

            AnimalBase animal = _animalPools[animalType].Get();
            animal.transform.position = new Vector3(at.x, 0, at.y);
            return animal;
        }

        public void Release<T>(AnimalBase animal) where T : AnimalBase
        {
            Type animalType = typeof(T);
            
            if (!_animalPools.ContainsKey(animalType))
            {
                Debug.LogError($"No pool found for type '{animalType.Name}'.");
                return;
            }

            _animalPools[animalType].Release(animal);
        }

        private AnimalBase CreateAnimal(Type animalType)
        {
            _animalPrefabs.TryGetValue(animalType, out GameObject prefab);
            if (prefab == null)
            {
                Debug.LogError($"No prefab registered for type '{animalType.Name}'.");
                return null;
            }

            AnimalBase animal = UnityEngine.Object.Instantiate(prefab).GetComponent<AnimalBase>();
		    animal.Construct(_allServices.Single<IBoundsReturnService>(), _allServices.Single<IDeathResolverService>(), _animalPools);

            return animal;
        }


        private void OnGetAnimal(AnimalBase animal)
        {
            animal.IsAlive = true;
            animal.gameObject.SetActive(true);
        }

        private void OnReleaseAnimal(AnimalBase animal) =>
            animal.gameObject.SetActive(false);

        private void OnDestroyAnimal(AnimalBase animal) =>
            UnityEngine.Object.Destroy(animal.gameObject);
    }
}
