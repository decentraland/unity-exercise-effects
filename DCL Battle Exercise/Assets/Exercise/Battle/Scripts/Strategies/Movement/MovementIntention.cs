using Exercise.Battle.Scripts.GameLoop.Systems;
using UnityEngine;

namespace Exercise.Battle.Scripts.Strategies.Movement
{
	public readonly struct MovementIntention : IIntention
	{
		public readonly Vector3 Direction;

		public MovementIntention(Vector3 direction)
		{
			Direction = direction;
		}
	}
}