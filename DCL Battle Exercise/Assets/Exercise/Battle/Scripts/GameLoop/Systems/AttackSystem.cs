using System.Collections.Generic;
using Exercise.Battle.Scripts.Strategies.Attack;
using Exercise.Battle.Scripts.Strategies.TargetSelection;
using Exercise.Battle.Scripts.Units;
using Exercise.Battle.Scripts.Units.Modules;
using UnityEngine;

namespace Exercise.Battle.Scripts.GameLoop.Systems
{
	public class AttackSystem
	{
		private readonly ITimeProvider _timeProvider;
		private readonly IntentionsRegistry<HitIntention> _hitIntentions;

		public AttackSystem(ITimeProvider timeProvider, IntentionsRegistry<HitIntention> hitIntentions)
		{
			_timeProvider = timeProvider;
			_hitIntentions = hitIntentions;
		}

		public void Update(IReadOnlyCollection<IUnit> units)
		{
			foreach (var unit in units)
			{
				if (!unit.TryGetModule(out AttackModule attackModule))
				{
					continue;
				}

				UpdateCooldown(attackModule);

				if (!unit.TryGetModule(out TargetSelectionModule targetModule) || targetModule.TargetIntention == null)
				{
					continue;
				}

				var targetIntention = targetModule.TargetIntention.Value;

				var attackPerformed = attackModule.Strategy.Execute(unit, attackModule, targetIntention, _hitIntentions);

				if (attackPerformed)
				{
					unit.SetTrigger(attackModule.AttackTriggerId);

					attackModule.SetAttackPerformed(_timeProvider.GetTime());
					UpdateCooldown(attackModule);
				}
			}
		}

		private void UpdateCooldown(AttackModule attackModule)
		{
			var onPostAttackDelay = attackModule.AttackPerformedTime + attackModule.Settings.PostAttackDelay > _timeProvider.GetTime();
			var onCooldown = attackModule.AttackPerformedTime + attackModule.Settings.AttackCooldown > _timeProvider.GetTime();

			attackModule.SetOnCooldown(onCooldown, onPostAttackDelay);
		}
	}
}