using Exercise.Battle.Scripts.Strategies.Attack;
using Exercise.Battle.Scripts.Strategies.Movement;
using Exercise.Battle.Scripts.Strategies.TargetSelection;
using UnityEngine;

namespace Exercise.Battle.Scripts.Strategies
{
	public class UnitStrategyEntry : MonoBehaviour, IUnitStrategyEntry
	{
		[field: SerializeField]
		public ArmyStrategy GlobalStrategy { get; private set; }
		
		[field: SerializeField]
		public UnitTargetSelectionStrategyBase TargetSelectionStrategy { get; private set; }
		
		[field: SerializeField]
		public UnitMovementStrategyBase MovementStrategy { get; private set; }
		
		[field: SerializeField]
		public UnitAttackStrategyBase AttackStrategy { get; private set; }
	}
}