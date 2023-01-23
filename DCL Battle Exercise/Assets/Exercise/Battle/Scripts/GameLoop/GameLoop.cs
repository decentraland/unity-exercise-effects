using System.Collections.Generic;
using Exercise.Battle.Scripts.Battle;
using Exercise.Battle.Scripts.GameLoop.Systems;
using Exercise.Battle.Scripts.Units;

namespace Exercise.Battle.Scripts.GameLoop
{
	public class GameLoop
	{
		private readonly ArrowsSystem _arrowsSystem;
		private readonly AttackSystem _attackSystem;
		private readonly IBattle _battle;
		private readonly DamageSystem _damageSystem;

		private readonly HashSet<IUnit> _deadUnits = new HashSet<IUnit>();
		private readonly DisposeDeadUnitsSystem _disposeDeadUnitsSystem;
		private readonly MovementSystem _movementSystem;
		private readonly SelectTargetSystem _selectTargetSystem;
		private readonly VictoryConditionsSystem _victoryConditionsSystem;

		public GameLoop(
			IBattle battle,
			SelectTargetSystem selectTargetSystem,
			MovementSystem movementSystem,
			ArrowsSystem arrowsSystem,
			AttackSystem attackSystem,
			DamageSystem damageSystem,
			DisposeDeadUnitsSystem disposeDeadUnitsSystem,
			VictoryConditionsSystem victoryConditionsSystem)
		{
			_battle = battle;
			_selectTargetSystem = selectTargetSystem;
			_movementSystem = movementSystem;
			_arrowsSystem = arrowsSystem;
			_attackSystem = attackSystem;
			_damageSystem = damageSystem;
			_disposeDeadUnitsSystem = disposeDeadUnitsSystem;
			_victoryConditionsSystem = victoryConditionsSystem;
		}

		public bool Update()
		{
			UpdateSelectTarget();
			UpdateMovement();

			UpdateArrows();
			UpdateAttack();
			UpdateDamage();

			DisposeDeadUnits();

			if (_victoryConditionsSystem.IsVictoryConditionMet())
			{
				return true;
			}

			return false;
		}
		
		private void UpdateSelectTarget()
		{
			foreach (var battleArmy in _battle.Armies)
			{
				_selectTargetSystem.Update(battleArmy.Units);
			}
		}

		private void UpdateMovement()
		{
			foreach (var battleArmy in _battle.Armies)
			{
				_movementSystem.Update(battleArmy.Units);
			}
			
			_battle.RecalculateUnitsCenter();
		}

		private void UpdateArrows()
		{
			_arrowsSystem.Update();
		}

		private void UpdateAttack()
		{
			foreach (var battleArmy in _battle.Armies)
			{
				_attackSystem.Update(battleArmy.Units);
			}
		}

		private void UpdateDamage()
		{
			_damageSystem.Update(_deadUnits);
		}

		private void DisposeDeadUnits()
		{
			_disposeDeadUnitsSystem.Execute(_deadUnits);
		}
	}
}