using CodeBase.Animals.MovementStrategies;
using CodeBase.Services;
using UnityEngine;

namespace CodeBase.Animals
{

    public abstract class AnimalBase : MonoBehaviour 
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private AnimalType _type;

        private IBoundsReturnService _boundsReturnService;
        private IDeathResolverService _deathResolverService;

        private IMovementStrategy _movementStrategy;

        public AnimalType Type => _type;
        public bool IsAlive { get; private set; } = true;

        public void Construct(IBoundsReturnService boundsReturnService, IDeathResolverService deathResolverService)
        {
            _boundsReturnService = boundsReturnService;
            _deathResolverService = deathResolverService;

            _movementStrategy = LoadMovementStrategy();
            _movementStrategy.Initialize();
        }

        protected virtual IMovementStrategy LoadMovementStrategy() =>
            GetComponent<IMovementStrategy>() ?? gameObject.AddComponent<LinearMovementStrategy>();

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
                _movementStrategy.ChangeDirectionTowards(transform.position + correctedDirection.Value);
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

        public void Die() =>
            IsAlive = false;
    }
}