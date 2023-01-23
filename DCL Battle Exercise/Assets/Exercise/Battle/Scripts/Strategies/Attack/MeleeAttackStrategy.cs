using Exercise.Battle.Scripts.Strategies.TargetSelection;
using Exercise.Battle.Scripts.Units;
using Exercise.Battle.Scripts.Units.Modules;

namespace Exercise.Battle.Scripts.Strategies.Attack
{
	public class MeleeAttackStrategy : UnitAttackStrategyBase
	{
		protected override bool ExecuteInternal(IUnit unit, AttackModule attackModule, TargetIntention targetIntention,
			IMutableIntentionsRegistry<HitIntention> hitIntentions)
		{
			AddHitIntention(hitIntentions, targetIntention.Target, attackModule.Settings);
			return true;
		}
	}
}