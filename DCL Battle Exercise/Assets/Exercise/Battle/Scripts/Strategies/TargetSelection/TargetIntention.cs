using Exercise.Battle.Scripts.GameLoop.Systems;
using Exercise.Battle.Scripts.Units;
using UnityEngine;

namespace Exercise.Battle.Scripts.Strategies.TargetSelection
{
	public readonly struct TargetIntention : IIntention
	{
		public readonly IUnit Target;
		public readonly Vector3 ToTarget;
		public readonly Vector3 ToTargetNormalized;

		public TargetIntention(IUnit target, Vector3 toTarget)
		{
			Target = target;
			ToTarget = toTarget;
			ToTargetNormalized = toTarget.normalized;
		}
	}
}