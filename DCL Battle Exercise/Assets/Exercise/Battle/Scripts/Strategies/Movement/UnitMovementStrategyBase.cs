using Exercise.Battle.Scripts.Strategies.TargetSelection;
using Exercise.Battle.Scripts.Units;
using Exercise.Battle.Scripts.Units.Modules;
using UnityEngine;

namespace Exercise.Battle.Scripts.Strategies.Movement
{
	public abstract class UnitMovementStrategyBase : MonoBehaviour
	{
		public MovementIntention? GetIntention(MovementModule movementModule, TargetIntention targetIntention, IUnit unit)
		{
			if (unit.TryGetModule(out AttackModule attackModule) && attackModule.OnPostAttackDelay)
				return null;

			var targetPosition = GetDirection(unit, attackModule, targetIntention);
			if (targetPosition.HasValue)
				return new MovementIntention(targetPosition.Value);

			return null;
		}

		/// <summary>
		/// Basic implementation to move towards the target
		/// </summary>
		protected virtual Vector3? GetDirection(IUnit source, AttackModule attackModule, TargetIntention targetIntention)
		{
			return targetIntention.ToTargetNormalized;
		}
	}
}