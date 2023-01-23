using Exercise.Battle.Scripts.Strategies;
using Exercise.Battle.Scripts.Units.Settings;
using JetBrains.Annotations;
using UnityEngine;

namespace Exercise.Battle.Scripts.Units.Modules
{
	public class HealthModule : UnitModuleBase<HealthSettings>, IUnitModule
	{
		[SerializeField]
		private string _hitTrigger = "Hit";

		[SerializeField]
		private string _deathTrigger = "Death";
		
		public int HitTriggerId { get; private set; }
		
		public int DeathTriggerId { get; private set; }
		
		public float CurrentHealth { get; set; }
		
		public bool DeathAnimationPlayed { get; private set; }

		public override void Initialize(IUnitStrategyEntry strategyEntry)
		{
			CurrentHealth = Settings.Health;
		}

		private void Awake()
		{
			HitTriggerId = Animator.StringToHash(_hitTrigger);
			DeathTriggerId = Animator.StringToHash(_deathTrigger);
		}
		
		[UsedImplicitly]
		private void OnDeathAnimFinished()
		{
			DeathAnimationPlayed = true;
		}
	}
}