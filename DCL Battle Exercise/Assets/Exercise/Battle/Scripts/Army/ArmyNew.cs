using System.Collections.Generic;
using Exercise.Battle.Scripts.Units;
using UnityEngine;

namespace Exercise.Battle.Scripts.Army
{
	public class ArmyNew : IArmy
	{
		private HashSet<IUnit> _aliveUnits;

		private IReadOnlyList<IArmy> _enemies;

		public ArmyNew(int index, Color color)
		{
			Index = index;
			Color = color;
		}

		public int Index { get; }

		public Color Color { get; }

		public IReadOnlyCollection<IUnit> Units => _aliveUnits;

		public Vector3 EnemiesCenter { get; private set; }

		public Vector3 Position { get; private set; }

		public void SetUnits(HashSet<IUnit> units, IReadOnlyList<IArmy> enemies)
		{
			_aliveUnits = units;
			_enemies = enemies;
		}

		public void CalculateCenter()
		{
			Position = _aliveUnits.CalculateCenter();
		}

		public void CalculateEnemiesCenter()
		{
			EnemiesCenter = _enemies.CalculateCenter();
		}

		public void RemoveUnit(IUnit unit)
		{
			_aliveUnits.Remove(unit);
		}
	}
}