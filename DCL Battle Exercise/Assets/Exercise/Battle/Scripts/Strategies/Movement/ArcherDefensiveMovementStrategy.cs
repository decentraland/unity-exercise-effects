using Exercise.Battle.Scripts.GameLoop.Systems;
using Exercise.Battle.Scripts.Strategies.TargetSelection;
using Exercise.Battle.Scripts.Units;
using Exercise.Battle.Scripts.Units.Modules;
using UnityEngine;

namespace Exercise.Battle.Scripts.Strategies.Movement
{
	public class ArcherDefensiveMovementStrategy : UnitMovementStrategyBase
	{
		protected override Vector3? GetDirection(IUnit source, AttackModule attackModule, TargetIntention targetIntention)
		{
			var movementDirection = Vector3.zero;

			var enemyCenter = source.AllyArmy.EnemiesCenter;
			var distanceToEnemyX = Mathf.Abs(enemyCenter.x - source.Position.x);

			if (distanceToEnemyX > attackModule.Settings.AttackRange)
			{
				if (enemyCenter.x < transform.position.x)
				{
					movementDirection = Vector3.left;
				}

				if (enemyCenter.x > transform.position.x)
				{
					movementDirection = Vector3.right;
				}
			}
			else
			{
				if (targetIntention.ToTarget.magnitude < attackModule.Settings.AttackRange)
				{
					var toNearest = targetIntention.ToTargetNormalized;
					toNearest.y = 0;

					var flank = Quaternion.Euler(0, 90, 0) * toNearest;
					var flanking = -Utility.CombineDirection(toNearest, flank);
					movementDirection = Utility.CombineDirection(movementDirection, flanking);
				}
				else
				{
					var toNearest = targetIntention.ToTargetNormalized;
					toNearest.y = 0;
					movementDirection = Utility.CombineDirection(movementDirection, -toNearest);
				}
			}

			return movementDirection;
		}
	}
}