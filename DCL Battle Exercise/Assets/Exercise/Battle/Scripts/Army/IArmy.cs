using System.Collections.Generic;
using Exercise.Battle.Scripts.Units;
using UnityEngine;

namespace Exercise.Battle.Scripts.Army
{
	public interface IArmy : IPositionProvider
	{
		int Index { get; }
		Color Color { get; }
		IReadOnlyCollection<IUnit> Units { get; }
		Vector3 EnemiesCenter { get; }
		void SetUnits(HashSet<IUnit> units, IReadOnlyList<IArmy> enemies);
		void CalculateCenter();
		void CalculateEnemiesCenter();
		void RemoveUnit(IUnit unit);
	}
}