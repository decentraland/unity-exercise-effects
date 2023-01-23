using System;
using Exercise.Battle.Scripts.Battle;
using Exercise.Battle.Scripts.Strategies.Movement;
using Exercise.Battle.Scripts.Units;
using UnityEngine;
using UnityEngine.Profiling;

namespace Exercise.Battle.Scripts.Rules
{
	[CreateAssetMenu(menuName = "Restrictions/Create KeepCloseToAllUnitsCenterRestriction", fileName = "KeepCloseToAllUnitsCenterRestriction",
		order = 1)]
	public class KeepCloseToAllUnitsCenterRestriction : MovementRestriction
	{
		[field: SerializeField]
		public float MaxDistance { get; private set; } = 80;

		private float _sqrDistance;

		private void OnEnable()
		{
			CalculateSquareDistance(MaxDistance, ref _sqrDistance);
		}

		public void SetMaxDistance(float distance)
		{
			MaxDistance = distance;
			CalculateSquareDistance(MaxDistance, ref _sqrDistance);
		}

		public override Vector3 RestrictPosition(IBattle battle, IUnit unit, MovementIntention movementIntention, Vector3 worldPosition)
		{
			Profiler.BeginSample($"{nameof(KeepCloseToAllUnitsCenterRestriction)}.{nameof(RestrictPosition)}");
			
			var battleCenter = battle.Position;
			var sqrDistance = Utility.SqrDistance(worldPosition, battleCenter);

			if (sqrDistance > _sqrDistance)
			{
				worldPosition = (worldPosition - battleCenter).normalized * MaxDistance + battleCenter;
			}
			Profiler.EndSample();
			return worldPosition;
		}
	}
}