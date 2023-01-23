using System.Collections.Generic;
using Exercise.Battle.Scripts.Units;
using UnityEngine;

namespace Exercise.Battle.Scripts.Army
{
	public class ArmyBuilder
	{
		private readonly UnitFactory _unitFactory;

		public ArmyBuilder(UnitFactory unitFactory)
		{
			_unitFactory = unitFactory;
		}

		public void Build(IArmy currentArmy, IReadOnlyList<IArmy> enemyArmies, IArmyNewModel model, Color color, Bounds spawnBounds)
		{
			var units = new HashSet<IUnit>();

			foreach (var modelUnit in model.GetUnits())
			{
				for (var i = 0; i < modelUnit.Item2; i++)
				{
					var position = global::Utils.GetRandomPosInBounds(spawnBounds);
					var unit = _unitFactory.Create(modelUnit.Item1, position, model.Strategy, color, currentArmy, enemyArmies);
					units.Add(unit);
				}
			}

			currentArmy.SetUnits(units, enemyArmies);
		}
	}
}