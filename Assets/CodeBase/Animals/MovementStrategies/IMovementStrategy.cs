using UnityEngine;

namespace CodeBase.Animals
{
    public interface IMovementStrategy
    {
        void Initialize();
        void PhysicsTick();
        void ChangeDirectionTowards(Vector3 target);
        void Stop();
    }
}