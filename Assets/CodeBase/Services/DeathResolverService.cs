using UnityEngine;
using CodeBase.Animals;

namespace CodeBase.Services
{
    public class DeathResolverService : IDeathResolverService
    {
        public void ResolveDeath(AnimalBase invocatorAnimal, AnimalBase collidedWithAnimal)
        {
            if(invocatorAnimal.Type == collidedWithAnimal.Type)
            {
                AnimalType animalsType = invocatorAnimal.Type;

                if (animalsType == AnimalType.Predator)
                    RandomlyKillOne(invocatorAnimal, collidedWithAnimal);
            }
            else
            {
                AnimalBase prey = invocatorAnimal.Type == AnimalType.Prey ? invocatorAnimal : collidedWithAnimal;
                prey.Die();
            }
        }

        private void RandomlyKillOne(AnimalBase invocatorAnimal, AnimalBase collidedWithAnimal)
        {
            if (Random.Range(0, 2) == 0)
                invocatorAnimal.Die();
            else
                collidedWithAnimal.Die();
        }
    }
}