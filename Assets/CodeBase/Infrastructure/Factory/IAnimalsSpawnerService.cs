using CodeBase.Services;

namespace CodeBase.Infrastructure.Factory
{
    public interface IAnimalsSpawnerService : IService
    {
        void StartOngoingSpawning();
    }
}
