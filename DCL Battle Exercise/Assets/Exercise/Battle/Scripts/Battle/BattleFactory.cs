using System.Collections.Generic;
using System.Linq;
using Exercise.Battle.Scripts.Army;

namespace Exercise.Battle.Scripts.Battle
{
	public class BattleFactory
	{
		private readonly ArmyBuilder _armyBuilder;
		
		public BattleFactory(ArmyBuilder armyBuilder)
		{
			_armyBuilder = armyBuilder;
		}
		
		public IBattle Create(BattleConfiguration battleConfiguration)
		{
			var armies = battleConfiguration.Armies.Select((army, i) => new ArmyNew(i, army.Color)).ToList();

			for (var i = 0; i < armies.Count; i++)
			{
				var currentArmy = armies[i];

				var enemyArmies = new List<IArmy>(armies);
				enemyArmies.RemoveAt(i);

				var configuration = battleConfiguration.Armies[i];
				
				_armyBuilder.Build(currentArmy, enemyArmies, configuration.Model, configuration.Color, configuration.SpawnBounds.bounds);
			}

			foreach (var army in armies)
			{
				army.CalculateEnemiesCenter();
			}

			return new Battle(armies);
		}
	}
}