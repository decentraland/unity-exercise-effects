using System.Collections.Generic;
using Exercise.Battle.Scripts.Army;
using Exercise.Battle.Scripts.Units;
using UnityEngine;

namespace Exercise.Battle.Scripts
{
	public static class Utility
	{
		public static Vector3 CalculateCenter<T>(this IReadOnlyCollection<T> positionProviders) where T : IPositionProvider
		{
			var center = Vector3.zero;

			foreach (var enemy in positionProviders)
			{
				center += enemy.Position;
			}

			return center / positionProviders.Count;
		}
		
		public static Vector3 CombineDirection(Vector3 a, Vector3 b)
		{
			return (a + b).normalized;
		}

		public static float SqrDistance(Vector3 a, Vector3 b)
		{
			float num1 = a.x - b.x;
			float num2 = a.y - b.y;
			float num3 = a.z - b.z;
			return num1 * num1 + num2 * num2 + num3 * num3;
		}

		public static float GetNearestUnit(IUnit source, IReadOnlyList<IArmy> armies, out IUnit nearestUnit)
		{
			var minDist = float.MaxValue;
			nearestUnit = null;

			for (var index = 0; index < armies.Count; index++)
			{
				var army = armies[index];
				foreach (var obj in army.Units)
				{
					var dist = SqrDistance(source.Position, obj.Position);

					if (dist < minDist)
					{
						minDist = dist;
						nearestUnit = obj;
					}
				}
			}

			return minDist;
		}
	}
}
