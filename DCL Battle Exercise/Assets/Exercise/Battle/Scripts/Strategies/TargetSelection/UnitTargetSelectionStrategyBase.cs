using System.Collections.Generic;
using Exercise.Battle.Scripts.Army;
using Exercise.Battle.Scripts.Units;
using UnityEngine;

namespace Exercise.Battle.Scripts.Strategies.TargetSelection
{
	public abstract class UnitTargetSelectionStrategyBase : MonoBehaviour
	{
		public TargetIntention? GetIntention(IUnit unit, IReadOnlyList<IArmy> enemyArmies)
		{
			var targetUnit = GetTarget(unit, enemyArmies);
			return targetUnit != null ? new TargetIntention(targetUnit, targetUnit.Position - unit.Position) : (TargetIntention?) null;
		}

		protected abstract IUnit GetTarget(IUnit source, IReadOnlyList<IArmy> enemyArmies);
	}
}