using Exercise.Battle.Scripts.Strategies.TargetSelection;
using Exercise.Battle.Scripts.Units;
using Exercise.Battle.Scripts.Units.Modules;
using UnityEngine;

namespace Exercise.Battle.Scripts.Strategies.Movement
{
	public class WarriorDefensiveMovementStrategy : UnitMovementStrategyBase
	{
		protected override Vector3? GetDirection(IUnit source, AttackModule attackModule, TargetIntention targetIntention)
		{
			return attackModule.OnCooldown
				? targetIntention.ToTargetNormalized * -1
				: base.GetDirection(source, attackModule, targetIntention);
		}
	}
}