using System;

namespace Exercise.Battle.Scripts.Units.Settings
{
	[Serializable]
	public struct AttackSettings
	{
		public float AttackRange;
		public float AttackCooldown;
		public float PostAttackDelay;
		public float Attack;
	}
}