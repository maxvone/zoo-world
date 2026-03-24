using UnityEngine;

namespace CodeBase.Services
{
    public interface IBoundsReturnService : IService
    {
		Vector3? GetCorrectionDirection(Vector3 position);
    }
}