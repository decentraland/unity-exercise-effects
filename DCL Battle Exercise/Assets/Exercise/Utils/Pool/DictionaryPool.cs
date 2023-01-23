using System.Collections.Generic;

namespace Exercise.Utils.Pool
{
	public class DictionaryPool<TKey, TValue> : StaticMemoryPool<DictionaryPool<TKey, TValue>, Dictionary<TKey, TValue>>
	{
		protected override void OnReturn(Dictionary<TKey, TValue> @object)
		{
			@object.Clear();
		}
	}
}