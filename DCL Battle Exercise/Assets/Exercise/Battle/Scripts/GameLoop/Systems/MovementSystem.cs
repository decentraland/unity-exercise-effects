using System.Collections.Generic;
using Exercise.Battle.Scripts.Battle;
using Exercise.Battle.Scripts.Rules;
using Exercise.Battle.Scripts.Units;
using Exercise.Battle.Scripts.Units.Modules;

namespace Exercise.Battle.Scripts.GameLoop.Systems
{
	/// <summary>
	///     Moves units with restrictions applied
	/// </summary>
	public class MovementSystem
	{
		private readonly IBattle _battle;
		private readonly IReadOnlyList<MovementRestriction> _movementRestrictions;
		private readonly ITimeProvider _timeProvider;

		public MovementSystem(IReadOnlyList<MovementRestriction> movementRestrictions, IBattle battle, ITimeProvider timeProvider)
		{
			_battle = battle;
			_timeProvider = timeProvider;
			_movementRestrictions = movementRestrictions;
		}

		public void Update(IReadOnlyCollection<IUnit> units)
		{
			foreach (var unit in units)
			{
				if (!unit.TryGetModule(out MovementModule movementModule) || movementModule.Strategy == null)
				{
					continue;
				}

				if (!unit.TryGetModule(out TargetSelectionModule targetIntention) || targetIntention.TargetIntention == null)
				{
					continue;
				}

				var movementAnimationFloat = 0f;
				var movementIntention = movementModule.Strategy.GetIntention(movementModule, targetIntention.TargetIntention.Value, unit);

				if (movementIntention != null)
				{

					var travelDistance = movementModule.Settings.Speed * _timeProvider.GetDeltaTime();

					var desiredPos = unit.Position +
					                 movementIntention.Value.Direction * travelDistance;

					for (var i = 0; i < _movementRestrictions.Count; i++)
					{
						desiredPos = _movementRestrictions[i].RestrictPosition(_battle, unit, movementIntention.Value, desiredPos);
					}

					movementAnimationFloat = (desiredPos - unit.Position).magnitude / travelDistance;

					unit.SetPosition(desiredPos);
				}
				unit.SetFloat(movementModule.MovementFloatId, movementAnimationFloat);
			}
		}
	}
}