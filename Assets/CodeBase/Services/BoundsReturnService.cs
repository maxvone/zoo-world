using UnityEngine;

namespace CodeBase.Services
{
	public class BoundsReturnService : IBoundsReturnService
	{
		private static readonly Vector2 DefaultBoundsMin = new(-8f, -5f);
		private static readonly Vector2 DefaultBoundsMax = new(8f, 5f);

		private readonly Vector2 _min;
		private readonly Vector2 _max;

		public BoundsReturnService() : this(DefaultBoundsMin, DefaultBoundsMax) { }

		public BoundsReturnService(Vector2 min, Vector2 max)
		{
			_min = min;
			_max = max;
		}

		/// <summary>
		/// Returns a correction direction if the animal is out of bounds, otherwise null.
		/// </summary>
		public Vector3? GetCorrectionDirection(Vector3 position)
		{
			float x = position.x;
			float z = position.z;

			bool outLeft = x < _min.x;
			bool outRight = x > _max.x;
			bool outBottom = z < _min.y;
			bool outTop = z > _max.y;

			if (!outLeft && !outRight && !outBottom && !outTop)
				return null;

			var correction = Vector3.zero;

			if (outLeft) correction.x = 1f;
			if (outRight) correction.x = -1f;
			if (outBottom) correction.z = 1f;
			if (outTop) correction.z = -1f;

			return correction.normalized;
		}
	}
}