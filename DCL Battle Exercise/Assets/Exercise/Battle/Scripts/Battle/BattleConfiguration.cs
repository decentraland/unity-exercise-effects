using System;
using System.Collections.Generic;
using Exercise.Battle.Scripts.Army;
using UnityEngine;

namespace Exercise.Battle.Scripts.Battle
{
	public class BattleConfiguration : MonoBehaviour
	{
		[Serializable]
		public class ArmyEntry
		{
			public ArmyNewModelSO Model;
			public BoxCollider SpawnBounds;
			public Color Color;
		}

		[field: SerializeField]
		public List<ArmyEntry> Armies;
	}
}