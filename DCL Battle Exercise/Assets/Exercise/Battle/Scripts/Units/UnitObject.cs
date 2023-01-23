using System.Collections.Generic;
using Exercise.Battle.Scripts.Strategies;
using UnityEngine;

namespace Exercise.Battle.Scripts.Units
{
	public class UnitObject : MonoBehaviour
	{
		[field: SerializeField]
		public Renderer Renderer { get; private set; }
		
		[field: SerializeField]
		public List<UnitStrategyEntry> AvailableStrategies { get; private set; }
		
		[field: SerializeField]
		public Animator Animator { get; private set; }
	}
}