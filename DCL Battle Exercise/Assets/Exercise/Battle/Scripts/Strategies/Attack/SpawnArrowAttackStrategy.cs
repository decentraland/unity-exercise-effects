using Exercise.Battle.Scripts.Projectiles;
using Exercise.Battle.Scripts.Strategies.TargetSelection;
using Exercise.Battle.Scripts.Units;
using Exercise.Battle.Scripts.Units.Modules;
using Exercise.Utils;
using UnityEngine;

namespace Exercise.Battle.Scripts.Strategies.Attack
{
	public class SpawnArrowAttackStrategy : UnitAttackStrategyBase
	{
		[SerializeField] private Arrow _arrowPrefab;

		private ArrowFactory _arrowsFactory;

		private ArrowFactory ArrowsFactory => _arrowsFactory ?? ServiceLocator.Instance.GetService<ArrowFactory>();

		protected override bool ExecuteInternal(IUnit unit, AttackModule attackModule, TargetIntention targetIntention,
			IMutableIntentionsRegistry<HitIntention> hitIntentions)
		{
			ArrowsFactory.Create(_arrowPrefab, unit.AllyArmy, unit.Position, targetIntention.Target.Position, attackModule.Settings,
				unit.EnemyArmies);
			return true;
		}
	}
}