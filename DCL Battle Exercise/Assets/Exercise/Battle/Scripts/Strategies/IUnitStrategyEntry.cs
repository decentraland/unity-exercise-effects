using Exercise.Battle.Scripts.Strategies.Attack;
using Exercise.Battle.Scripts.Strategies.Movement;
using Exercise.Battle.Scripts.Strategies.TargetSelection;

namespace Exercise.Battle.Scripts.Strategies
{
	public interface IUnitStrategyEntry
	{
		UnitTargetSelectionStrategyBase TargetSelectionStrategy { get; }
		UnitMovementStrategyBase MovementStrategy { get; }
		UnitAttackStrategyBase AttackStrategy { get; }
	}
}