using System.Collections.Generic;
using Exercise.Battle.Scripts.Army;
using UnityEngine;

namespace Exercise.Battle.Scripts.Battle
{
	public class Battle : IBattle
	{
		public IReadOnlyList<IArmy> Armies { get; }

		public Battle(IReadOnlyList<IArmy> armies)
		{
			Armies = armies;
		}

		/// <summary>
		/// All units center
		/// </summary>
		public Vector3 Position { get; private set; }

		public void RecalculateUnitsCenter()
		{
			foreach (var battleArmy in Armies)
			{
				battleArmy.CalculateCenter();
			}

			foreach (var battleArmy in Armies)
			{
				battleArmy.CalculateEnemiesCenter();
			}

			Position = Armies.CalculateCenter();
		}
	}
}