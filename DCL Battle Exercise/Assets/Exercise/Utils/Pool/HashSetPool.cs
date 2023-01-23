using System.Collections.Generic;

namespace Exercise.Utils.Pool
{
	public class HashSetPool<T> : StaticMemoryPool<HashSetPool<T>, HashSet<T>>
	{
		protected override void OnReturn(HashSet<T> @object)
		{
			@object.Clear();
		}
	}
}