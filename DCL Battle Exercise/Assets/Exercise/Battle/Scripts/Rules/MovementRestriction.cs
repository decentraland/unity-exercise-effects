using Exercise.Battle.Scripts.Battle;
using Exercise.Battle.Scripts.Strategies.Movement;
using Exercise.Battle.Scripts.Units;
using UnityEngine;

namespace Exercise.Battle.Scripts.Rules
{
	public abstract class MovementRestriction : ScriptableObject
	{
		public abstract Vector3 RestrictPosition(IBattle battle, IUnit unit, MovementIntention movementIntention, Vector3 worldPosition);

		protected static void CalculateSquareDistance(float distance, ref float target)
		{
			target = distance * distance;
		}
	}
}