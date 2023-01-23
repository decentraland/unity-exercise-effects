using System.Collections.Generic;
using Exercise.Battle.Scripts.Army;
using Exercise.Battle.Scripts.GameLoop.Systems;
using Exercise.Battle.Scripts.Units;
using Exercise.Battle.Scripts.Units.Settings;
using Exercise.Utils;
using Exercise.Utils.Pool;
using UnityEngine;

namespace Exercise.Battle.Scripts.Projectiles
{
	public class ArrowFactory : ArmyEntityFactory, IService
	{
		private readonly ArrowsSystem _arrowsSystem;
		private readonly PrefabPool _prefabPool;

		public ArrowFactory(ArrowsSystem arrowsSystem, PrefabPool prefabPool)
		{
			_arrowsSystem = arrowsSystem;
			_prefabPool = prefabPool;
		}

		public Arrow Create(Arrow prefab, IArmy sourceArmy, Vector3 position, Vector3 target, AttackSettings attackSettings,
			IReadOnlyList<IArmy> enemyArmies)
		{
			var arrow = _prefabPool.Get(null, prefab);
			arrow.transform.position = position;

			arrow.SetTarget(attackSettings, target, enemyArmies);

			SetColor(arrow.Renderer, sourceArmy.Color);
			_arrowsSystem.RegisterArrow(arrow);

			return arrow;
		}
	}
}