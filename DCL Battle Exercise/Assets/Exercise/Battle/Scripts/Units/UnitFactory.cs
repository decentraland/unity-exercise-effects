using System.Collections.Generic;
using System.Linq;
using Exercise.Battle.Scripts.Army;
using Exercise.Battle.Scripts.Units.Modules;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Exercise.Battle.Scripts.Units
{
	public class UnitFactory : ArmyEntityFactory
	{
		public IUnit Create(UnitObject prefab, Vector3 worldPosition, ArmyStrategy strategy, Color color, IArmy allyArmy, IReadOnlyList<IArmy> enemyArmies)
		{
			var unitInstance = Object.Instantiate(prefab);

			// There is an assumption made that UnitSettings may contain any number of custom modules
			var modules = unitInstance.GetComponents<IUnitModule>();
			var unitStrategy = unitInstance.AvailableStrategies.First(s => s.GlobalStrategy == strategy);

			// Activate selected strategy
			for (var i = 0; i < modules.Length; i++)
			{
				modules[i].Initialize(unitStrategy);
			}
			
			SetColor(unitInstance.Renderer, color);

			var unit = new UnitFacade(unitInstance, modules, enemyArmies, allyArmy);
			unit.SetPosition(worldPosition);
			return unit;
		}
	}
}