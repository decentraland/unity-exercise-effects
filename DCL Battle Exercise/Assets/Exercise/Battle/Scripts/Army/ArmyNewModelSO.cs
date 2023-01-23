using System;
using System.Collections.Generic;
using System.Linq;
using Exercise.Battle.Scripts.Units;
using UnityEngine;

namespace Exercise.Battle.Scripts.Army
{
	[CreateAssetMenu(menuName = "Create ArmyNewModelSO", fileName = "ArmyNewModelSO", order = 0)]
	public class ArmyNewModelSO : ScriptableObject, IArmyNewModel
	{
		[Serializable]
		private class Entry
		{
			public UnitObject Unit;

			[ReadOnly]
			public int Count = 100;
		}

		[SerializeField]
		private List<Entry> _units = new List<Entry>();

		public int GetCount(UnitObject unitObject)
		{
			return _units.Find(u => u.Unit == unitObject)?.Count ?? 0;
		}

		public IEnumerable<(UnitObject, int)> GetUnits()
		{
			return _units.Select(u => (u.Unit, u.Count));
		}

		[field: SerializeField, ReadOnly]
		public ArmyStrategy Strategy { get; set; }

		public void SetUnits(UnitObject unit, int count)
		{
			var entry = _units.Find(e => e.Unit == unit);
			if (entry != null)
			{
				entry.Count = count;
			}
			else
			{
				_units.Add(new Entry { Unit = unit, Count = count });
			}
		}
	}
}