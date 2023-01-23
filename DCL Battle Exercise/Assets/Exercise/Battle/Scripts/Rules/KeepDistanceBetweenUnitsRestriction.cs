using Exercise.Battle.Scripts.Battle;
using Exercise.Battle.Scripts.Strategies.Movement;
using Exercise.Battle.Scripts.Units;
using UnityEngine;
using UnityEngine.Profiling;

namespace Exercise.Battle.Scripts.Rules
{
	[CreateAssetMenu(menuName = "Restrictions/Create KeepDistanceBetweenUnitsRestriction", fileName = "KeepDistanceBetweenUnitsRestriction",
		order = 0)]
	public class KeepDistanceBetweenUnitsRestriction : MovementRestriction
	{
		private float _sqrDistance;

		[field: SerializeField]
		public float Distance { get; private set; } = 2;

		private void OnEnable()
		{
			CalculateSquareDistance(Distance, ref _sqrDistance);
		}

		public void SetDistance(float distance)
		{
			Distance = distance;
			CalculateSquareDistance(distance, ref _sqrDistance);
		}

		public override Vector3 RestrictPosition(IBattle battle, IUnit unit, MovementIntention movementIntention, Vector3 worldPosition)
		{
			Profiler.BeginSample($"{nameof(KeepDistanceBetweenUnitsRestriction)}.{nameof(RestrictPosition)}");

			for (var i = 0; i < battle.Armies.Count; i++)
			{
				var battleArmy = battle.Armies[i];
				foreach (var armyUnit in battleArmy.Units)
				{
					if (armyUnit != unit)
					{
						var otherUnitPos = armyUnit.Position;
						var sqrDistance = Utility.SqrDistance(worldPosition, otherUnitPos);

						if (sqrDistance < _sqrDistance)
						{
							worldPosition = (worldPosition - otherUnitPos).normalized * Distance + otherUnitPos;
						}
					}
				}
			}

			Profiler.EndSample();

			return worldPosition;
		}
	}
}
