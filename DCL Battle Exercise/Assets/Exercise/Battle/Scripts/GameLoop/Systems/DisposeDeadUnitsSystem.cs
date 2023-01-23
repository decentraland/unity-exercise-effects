using System.Collections.Generic;
using Exercise.Battle.Scripts.Units;
using Exercise.Battle.Scripts.Units.Modules;
using UnityEngine;

namespace Exercise.Battle.Scripts.GameLoop.Systems
{
	public class DisposeDeadUnitsSystem
	{
		public void Execute(HashSet<IUnit> deadUnits)
		{
			using var safeCopy = deadUnits.GetSafeCopy();

			for (var i = 0; i < safeCopy.rentedCopy.Count; i++)
			{
				var unit = safeCopy.rentedCopy[i];
				if (unit.TryGetModule(out HealthModule healthModule) && healthModule.DeathAnimationPlayed)
				{
					Object.Destroy(unit.GameObject);
					deadUnits.Remove(unit);
				}
			}
		}
	}
}