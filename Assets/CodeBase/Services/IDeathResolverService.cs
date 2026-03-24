using CodeBase.Animals;

namespace CodeBase.Services
{
    public interface IDeathResolverService : IService
    {
        void ResolveDeath(AnimalBase invocatorAnimal, AnimalBase collidedWithAnimal);
    }
}