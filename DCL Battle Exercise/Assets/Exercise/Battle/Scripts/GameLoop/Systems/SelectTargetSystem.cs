using System.Collections.Generic;
using Exercise.Battle.Scripts.Units;
using Exercise.Battle.Scripts.Units.Modules;

namespace Exercise.Battle.Scripts.GameLoop.Systems
{
	public class SelectTargetSystem
	{
		public void Update(IReadOnlyCollection<IUnit> units)
		{
			foreach (var unit in units)
			{
				if (!unit.TryGetModule(out TargetSelectionModule targetSelectionModule))
				{
					continue;
				}

				var target = targetSelectionModule.Strategy.GetIntention(unit, unit.EnemyArmies);
				targetSelectionModule.SetTargetIntention(target);
			}
		}
	}
}