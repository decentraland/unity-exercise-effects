using Exercise.Battle.Scripts.Strategies;
using Exercise.Battle.Scripts.Strategies.Attack;
using Exercise.Battle.Scripts.Units.Settings;
using UnityEngine;

namespace Exercise.Battle.Scripts.Units.Modules
{
	public class AttackModule : UnitModuleBase<AttackSettings>, IUnitModule
	{
		[SerializeField]
		private string _attackTrigger;

		public int AttackTriggerId { get; private set; }

		public float AttackPerformedTime { get; private set; } = float.MinValue;

		public bool OnCooldown { get; private set; }

		public bool OnPostAttackDelay { get; private set; }

		public UnitAttackStrategyBase Strategy { get; private set; }

		private void Awake()
		{
			AttackTriggerId = Animator.StringToHash(_attackTrigger);
		}

		public override void Initialize(IUnitStrategyEntry strategyEntry)
		{
			Strategy = strategyEntry.AttackStrategy;
		}

		public void SetAttackPerformed(float attackPerformedTime)
		{
			AttackPerformedTime = attackPerformedTime;
		}

		public void SetOnCooldown(bool onCooldown, bool onPostAttackDelay)
		{
			OnCooldown = onCooldown;
			OnPostAttackDelay = onPostAttackDelay;
		}
	}
}