using System.Collections.Generic;
using Exercise.Battle.Scripts.Army;
using Exercise.Battle.Scripts.Units.Settings;
using UnityEngine;

namespace Exercise.Battle.Scripts.Projectiles
{
	public class Arrow : MonoBehaviour
	{
		[field: SerializeField]
		public MovementSettings MovementSettings { get; private set; }
		
		[field: SerializeField]
		public Renderer Renderer { get; private set; }
		
		public Vector3 Target { get; private set; }
		
		public IReadOnlyList<IArmy> EnemyArmies { get; private set; }
		
		public AttackSettings AttackSettings { get; private set; }

		public void SetTarget(AttackSettings attackSettings, Vector3 target, IReadOnlyList<IArmy> enemyArmies)
		{
			AttackSettings = attackSettings;
			Target = target;
			EnemyArmies = enemyArmies;
		}
	}
}