using UnityEngine;

namespace CodeBase.Animals.MovementStrategies
{
    public class LinearMovementStrategy : MonoBehaviour, IMovementStrategy
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _speed;

        private Vector3 _direction;
        private bool _stopped;

        public void Initialize()
        {
            _direction = GetRandomDirection();

            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        }

        public void PhysicsTick()
        {
            if (_stopped)
                return;

            _rigidbody.linearVelocity = _direction * _speed;
        }

        public void ChangeDirectionTowards(Vector3 target)
        {
            var toTarget = (target - _rigidbody.position).normalized;
            toTarget.y = 0;
            _direction = toTarget;
        }

        public void Stop()
        {
            _stopped = true;

            if (_rigidbody != null)
                _rigidbody.linearVelocity = Vector3.zero;
        }

        private static Vector3 GetRandomDirection()
        {
            var angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            return new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle));
        }
    }
}