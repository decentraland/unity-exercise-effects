using System.Collections.Generic;
using Exercise.Battle.Scripts.Units;

namespace Exercise.Battle.Scripts.Army
{
	public interface IArmyNewModel
	{
		public int GetCount(UnitObject unitObject);

		public IEnumerable<(UnitObject, int)> GetUnits();

		ArmyStrategy Strategy { get; set; }

		public void SetUnits(UnitObject unit, int count);
	}
}