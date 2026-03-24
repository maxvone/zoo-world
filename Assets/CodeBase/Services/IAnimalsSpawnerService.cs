using CodeBase.Services;

namespace CodeBase.Infrastructure.Services
{
    public interface IAnimalsSpawnerService : IService
    {
        void StartOngoingSpawning();
    }
}
