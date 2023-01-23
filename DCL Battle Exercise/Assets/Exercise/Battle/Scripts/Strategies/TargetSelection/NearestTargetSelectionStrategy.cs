using System.Collections.Generic;
using Exercise.Battle.Scripts.Army;
using Exercise.Battle.Scripts.Units;

namespace Exercise.Battle.Scripts.Strategies.TargetSelection
{
	public class NearestTargetSelectionStrategy : UnitTargetSelectionStrategyBase
	{
		protected override IUnit GetTarget(IUnit source, IReadOnlyList<IArmy> enemyArmies)
		{
			Utility.GetNearestUnit(source, enemyArmies, out var target);
			return target;
		}
	}
}