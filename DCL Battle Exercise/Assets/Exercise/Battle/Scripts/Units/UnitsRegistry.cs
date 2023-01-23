using System.Collections.Generic;
using UnityEngine;

namespace Exercise.Battle.Scripts.Units
{
	[CreateAssetMenu(menuName = "Create UnitsRegistry", fileName = "UnitsRegistry", order = 0)]
	public class UnitsRegistry : ScriptableObject
	{
		[SerializeField] private List<UnitObject> _units;

		public IReadOnlyList<UnitObject> Units => _units;
	}
}