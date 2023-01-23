using System.Collections.Generic;

namespace Exercise.Utils.Pool
{
	public class ListPool<T> : StaticMemoryPool<ListPool<T>, List<T>>
	{
		protected override void OnReturn(List<T> @object)
		{
			@object.Clear();
		}
	}
}