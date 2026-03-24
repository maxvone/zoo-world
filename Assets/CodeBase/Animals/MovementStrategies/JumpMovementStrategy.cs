using UnityEngine;

namespace CodeBase.Animals.MovementStrategies
{
    public class JumpMovementStrategy : MonoBehaviour, IMovementStrategy
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _jumpInterval;
        [SerializeField] private float _jumpForce;

        private float _timer;
        private bool _stopped;
        private Vector3? _forcedDirection;

        public void Initialize()
        {
            _rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        }

        public void PhysicsTick()
        {
            if (_stopped)
                return;

            JumpIntervalTick();
            TryJump();
        }

        private void JumpIntervalTick() =>
            _timer += Time.fixedDeltaTime;

        private void TryJump()
        {
            if (_timer >= _jumpInterval)
            {
                _timer = 0f;
                Jump();
            }
        }

        public void ChangeDirectionTowards(Vector3 target)
        {
            _forcedDirection = (target - _rigidbody.position).normalized;
            _forcedDirection = new Vector3(_forcedDirection.Value.x, 0f, _forcedDirection.Value.z);

            _timer = _jumpInterval;
        }

        public void Stop()
        {
            _stopped = true;
            if (_rigidbody != null)
                _rigidbody.linearVelocity = Vector3.zero;
        }

        private void Jump()
        {
            _rigidbody.linearVelocity = Vector3.zero;

            Vector3 dir;
            if (_forcedDirection.HasValue)
            {
                dir = _forcedDirection.Value;
                _forcedDirection = null;
            }
            else
            {
                dir = GetRandomDirection();
            }

            _rigidbody.AddForce(dir * _jumpForce, ForceMode.Impulse);
        }

        private static Vector3 GetRandomDirection()
        {
            var angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            return new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle));
        }
    }
}