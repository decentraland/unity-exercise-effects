using UnityEngine;

namespace Exercise.Battle.Scripts
{
	public interface IPositionProvider
	{
		Vector3 Position { get; }
	}
}