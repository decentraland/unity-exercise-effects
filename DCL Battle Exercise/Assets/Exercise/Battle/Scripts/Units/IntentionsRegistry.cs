using System.Collections.Generic;
using Exercise.Battle.Scripts.GameLoop.Systems;
using Exercise.Utils.Pool;

namespace Exercise.Battle.Scripts.Units
{
	public interface IMutableIntentionsRegistry<in T> where T : IIntention
	{
		void Add(IUnit unit, T intention);
	}
	
	public class IntentionsRegistry<T> : IMutableIntentionsRegistry<T> where T : IIntention
	{
		private readonly Dictionary<IUnit, List<T>> _intentions = new Dictionary<IUnit, List<T>>();

		public void Add(IUnit unit, T intention)
		{
			if (!_intentions.TryGetValue(unit, out var intentionsList))
			{
				_intentions[unit] = intentionsList = ListPool<T>.Rent();
			}

			intentionsList.Add(intention);
		}

		public IReadOnlyDictionary<IUnit, List<T>> GetIntentions()
		{
			return _intentions;
		}

		public void Clear()
		{
			foreach (var intention in _intentions)
			{
				ListPool<T>.Return(intention.Value);
			}

			_intentions.Clear();
		}
	}
}