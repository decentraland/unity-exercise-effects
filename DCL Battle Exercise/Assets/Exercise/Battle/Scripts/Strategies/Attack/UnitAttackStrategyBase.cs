using System.Collections.Generic;
using Exercise.Battle.Scripts.Strategies.TargetSelection;
using Exercise.Battle.Scripts.Units;
using Exercise.Battle.Scripts.Units.Modules;
using Exercise.Battle.Scripts.Units.Settings;
using UnityEngine;

namespace Exercise.Battle.Scripts.Strategies.Attack
{
	public abstract class UnitAttackStrategyBase : MonoBehaviour
	{
		public bool Execute(IUnit unit, AttackModule attackModule, TargetIntention targetIntention,
			IMutableIntentionsRegistry<HitIntention> hitIntentions)
		{
			if (attackModule.OnCooldown)
			{
				return false;
			}

			if (Vector3.Distance(unit.Position, targetIntention.Target.Position) > attackModule.Settings.AttackRange)
			{
				return false;
			}

			return ExecuteInternal(unit, attackModule, targetIntention, hitIntentions);
		}

		protected static void AddHitIntention(IMutableIntentionsRegistry<HitIntention> hitIntentions, IUnit target, AttackSettings attackSettings)
		{
			hitIntentions.Add(target, new HitIntention(attackSettings));
		}

		protected abstract bool ExecuteInternal(IUnit unit, AttackModule attackModule, TargetIntention targetIntention,
			IMutableIntentionsRegistry<HitIntention> hitIntentions);
	}
}