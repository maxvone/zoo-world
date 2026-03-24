using UnityEngine;
using CodeBase.Animals;
using CodeBase.UI.Services.Factory;

namespace CodeBase.Services
{
    public class DeathResolverService : IDeathResolverService
    {
        private readonly IUiFactory _uiFactory;
        private int _deadPreysCounter = 0;
        private int _deadPredatorsCounter = 0;

        public DeathResolverService(IUiFactory uiFactory)
        {
            _uiFactory = uiFactory;
        }

        public void ResolveDeath(AnimalBase invocatorAnimal, AnimalBase collidedWithAnimal)
        {
            if(invocatorAnimal.Type == collidedWithAnimal.Type)
            {
                AnimalType animalsType = invocatorAnimal.Type;

                if (animalsType == AnimalType.Predator)
                {
                    RandomlyKillPredator(invocatorAnimal, collidedWithAnimal);
                    _uiFactory.Hud.UpdateDeadPredatorsCounter(_deadPredatorsCounter);
                }
            }
            else
            {
                AnimalBase prey = invocatorAnimal.Type == AnimalType.Prey ? invocatorAnimal : collidedWithAnimal;
                KillPrey(prey);
                _uiFactory.Hud.UpdateDeadPreysCounter(_deadPreysCounter);
            }
        }

        private void KillPrey(AnimalBase prey)
        {
            prey.Die();
            _deadPreysCounter++;
        }

        private void RandomlyKillPredator(AnimalBase invocatorAnimal, AnimalBase collidedWithAnimal)
        {
            if (Random.Range(0, 2) == 0)
                invocatorAnimal.Die();
            else
                collidedWithAnimal.Die();

            _deadPredatorsCounter++;
        }
    }
}