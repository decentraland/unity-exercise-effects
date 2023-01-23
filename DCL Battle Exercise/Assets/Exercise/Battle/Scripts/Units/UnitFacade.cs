using System;
using System.Collections.Generic;
using System.Linq;
using Exercise.Battle.Scripts.Army;
using Exercise.Battle.Scripts.Units.Modules;
using UnityEngine;

namespace Exercise.Battle.Scripts.Units
{
	public class UnitFacade : IUnit
	{
		private readonly Dictionary<Type, IUnitModule> _modules;
		private readonly UnitObject _objectInstance;
		private readonly Transform _root;

		private Vector3 _position;

		public UnitFacade(UnitObject objectInstance, IReadOnlyList<IUnitModule> modules, IReadOnlyList<IArmy> enemyArmy,
			IArmy allyArmy)
		{
			_objectInstance = objectInstance;
			EnemyArmies = enemyArmy;
			AllyArmy = allyArmy;
			_root = _objectInstance.transform;

			_modules = modules.ToDictionary(m => m.GetType());
		}

		public void SetPosition(Vector3 position)
		{
			_root.position = position;
			_position = position;
		}

		public void SetTrigger(int triggerId)
		{
			_objectInstance.Animator.SetTrigger(triggerId);
		}

		public void SetFloat(int floatId, float value)
		{
			_objectInstance.Animator.SetFloat(floatId, value);
		}

		public GameObject GameObject => _objectInstance.gameObject;

		public Vector3 Position => _position;

		public IReadOnlyList<IArmy> EnemyArmies { get; }

		public IArmy AllyArmy { get; }

		public bool TryGetModule<T>(out T module) where T : IUnitModule
		{
			if (_modules.TryGetValue(typeof(T), out var abstractModule))
			{
				module = (T) abstractModule;
				return true;
			}

			module = default;
			return false;
		}

		public void Dispose()
		{
		}
	}
}