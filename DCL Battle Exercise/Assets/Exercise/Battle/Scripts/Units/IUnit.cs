using System;
using System.Collections.Generic;
using Exercise.Battle.Scripts.Army;
using Exercise.Battle.Scripts.GameLoop.Systems;
using Exercise.Battle.Scripts.Units.Modules;
using UnityEngine;

namespace Exercise.Battle.Scripts.Units
{
	public interface IUnit : IDisposable, IPositionProvider
	{
		IReadOnlyList<IArmy> EnemyArmies { get; }
		
		IArmy AllyArmy { get; }

		void SetTrigger(int triggerId);

		void SetFloat(int floatId, float value);

		void SetPosition(Vector3 position);

		public GameObject GameObject { get; }

		bool TryGetModule<T>(out T module) where T : IUnitModule;
	}
}