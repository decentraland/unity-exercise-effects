using System.Collections.Generic;
using Exercise.Battle.Scripts.Army;

namespace Exercise.Battle.Scripts.Battle
{
	public interface IBattle : IPositionProvider
	{
		IReadOnlyList<IArmy> Armies { get; }

		void RecalculateUnitsCenter();
	}
}