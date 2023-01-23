using UnityEngine;

namespace Exercise.Battle.Scripts
{
	public class ScaledTimeProvider : ITimeProvider
	{
		public float GetTime()
		{
			return Time.time;
		}

		public float GetDeltaTime()
		{
			return Time.deltaTime;
		}
	}
}