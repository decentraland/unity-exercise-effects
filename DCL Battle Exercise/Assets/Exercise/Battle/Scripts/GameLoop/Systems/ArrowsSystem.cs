using System.Collections.Generic;
using Exercise.Battle.Scripts.Army;
using Exercise.Battle.Scripts.Projectiles;
using Exercise.Battle.Scripts.Strategies.Attack;
using Exercise.Battle.Scripts.Units;
using Exercise.Battle.Scripts.Units.Settings;
using Exercise.Utils;
using Exercise.Utils.Pool;
using UnityEngine;

namespace Exercise.Battle.Scripts.GameLoop.Systems
{
	public class ArrowsSystem
	{
		private readonly HashSet<Arrow> _arrows = new HashSet<Arrow>();

		private readonly PrefabPool _prefabPool;
		private readonly ITimeProvider _timeProvider;
		private readonly IntentionsRegistry<HitIntention> _hitsIntentions;

		public ArrowsSystem(PrefabPool prefabPool, ITimeProvider timeProvider, IntentionsRegistry<HitIntention> hitsIntentions)
		{
			_prefabPool = prefabPool;
			_timeProvider = timeProvider;
			_hitsIntentions = hitsIntentions;
		}

		public void RegisterArrow(Arrow arrow)
		{
			_arrows.Add(arrow);
		}

		public void Update()
		{
			var safeList = _arrows.GetSafeCopy();

			foreach (var arrow in safeList.rentedCopy)
			{
				var arrowTransform = arrow.transform;
				var arrowPos = arrowTransform.position;

				var direction = (arrow.Target - arrowPos).normalized;

				var travelSpeed = arrow.MovementSettings.Speed * _timeProvider.GetDeltaTime();
				var travelVector = direction * travelSpeed;

				arrowTransform.position = arrowPos += travelVector;
				arrowTransform.forward = direction;

				foreach (var a in arrow.EnemyArmies)
				{
					foreach (var unit in a.Units)
					{
						var dist = Vector3.Distance(arrowPos, unit.Position);
						if (dist < travelSpeed)
						{
							_hitsIntentions.Add(unit, new HitIntention(arrow.AttackSettings));
							RemoveArrow(arrow);
							return;
						}
					}
				}

				if (Vector3.Distance(arrowPos, arrow.Target) < travelSpeed)
				{
					RemoveArrow(arrow);
				}
			}
		}

		private void RemoveArrow(Arrow arrow)
		{
			_arrows.Remove(arrow);
			_prefabPool.Release(arrow);
		}
	}
}