using Exercise.Battle.Scripts.Strategies;
using Exercise.Battle.Scripts.Strategies.Movement;
using Exercise.Battle.Scripts.Units.Settings;
using UnityEngine;

namespace Exercise.Battle.Scripts.Units.Modules
{
	public class MovementModule : UnitModuleBase<MovementSettings>, IUnitModule
	{
		[SerializeField]
		private string _movementFloatName = "MovementSpeed";

		public int MovementFloatId { get; private set; }

		public UnitMovementStrategyBase Strategy { get; private set; }

		private void Awake()
		{
			MovementFloatId = Animator.StringToHash(_movementFloatName);
		}

		public override void Initialize(IUnitStrategyEntry entry)
		{
			Strategy = entry.MovementStrategy;
		}
	}
}