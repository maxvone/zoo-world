using System;
using System.Collections.Generic;
using CodeBase.Services;
using UnityEngine;
using UnityEngine.Pool;

namespace CodeBase.Animals
{

    [RequireComponent(typeof(IMovementStrategy))]
    public abstract class AnimalBase : MonoBehaviour 
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private AnimalType _type;

        public Action Fed;

        private IBoundsReturnService _boundsReturnService;
        private IDeathResolverService _deathResolverService;
        private Dictionary<Type, IObjectPool<AnimalBase>> _animalPools;
        private IMovementStrategy _movementStrategy;

        public AnimalType Type => _type;
        public bool IsAlive { get; set; } = true;

        public void Construct(IBoundsReturnService boundsReturnService, IDeathResolverService deathResolverService,
            Dictionary<Type, IObjectPool<AnimalBase>> animalPools)
        {
            _boundsReturnService = boundsReturnService;
            _deathResolverService = deathResolverService;
            _animalPools = animalPools;

            InitializeMovementStrategy();
        }

        private void InitializeMovementStrategy()
        {
            _movementStrategy = GetComponent<IMovementStrategy>();
            _movementStrategy.Initialize();
        }

        protected virtual void FixedUpdate()
        {
            if (!IsAlive)
                return;

            _movementStrategy.PhysicsTick();
            CheckBounds();
        }

        private void CheckBounds()
        {
            var correctedDirection = _boundsReturnService.GetCorrectionDirection(transform.position);

            if (correctedDirection.HasValue)
            {
                var offsetDirection = ApplyAngleOffset(correctedDirection.Value);
                _movementStrategy.ChangeDirectionTowards(transform.position + offsetDirection);
            }
        }

        private Vector3 ApplyAngleOffset(Vector3 direction)
        {
            float angle = Mathf.Atan2(direction.z, direction.x);
            float angleOffset = UnityEngine.Random.Range(-30f, 30f) * Mathf.Deg2Rad;
            float newAngle = angle + angleOffset;
            return new Vector3(Mathf.Cos(newAngle), 0f, Mathf.Sin(newAngle));
        }

        protected virtual void OnCollisionEnter(Collision collision)
        {
            if (!IsAlive) return;

            var otherAnimal = collision.gameObject.GetComponent<AnimalBase>();
            if (otherAnimal == null || !otherAnimal.IsAlive)
                return;

            HandleCollisionWith(otherAnimal);
        }

        private void HandleCollisionWith(AnimalBase otherAnimal) =>
            _deathResolverService.ResolveDeath(this, otherAnimal);

        public void Die()
        {
            IsAlive = false;
            _animalPools[GetType()].Release(this);
        }

        public void Feed() =>
            Fed?.Invoke();
    }
}