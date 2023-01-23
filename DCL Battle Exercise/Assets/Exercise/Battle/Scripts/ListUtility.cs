using System;
using System.Collections.Generic;
using Exercise.Utils.Pool;

namespace Exercise.Battle.Scripts
{
	public static class ListUtility
	{
		public struct ListPoolToken<T> : IDisposable
		{
			public List<T> rentedCopy;

			public ListPoolToken(IEnumerable<T> original)
			{
				rentedCopy = ListPool<T>.Rent();
				rentedCopy.AddRange(original);
			}

			public void Dispose()
			{
				ListPool<T>.Return(rentedCopy);
				rentedCopy = null;
			}

			public static implicit operator List<T>(ListPoolToken<T> token)
			{
				return token.rentedCopy;
			}
		}

		public static ListPoolToken<T> GetSafeCopy<T>(this IEnumerable<T> original)
		{
			return new ListPoolToken<T>(original);
		}
		
		public static void RemoveAtSwapBack<T>(this IList<T> list, int index, out T swappedElement)
		{
			swappedElement = list[list.Count - 1];
			(list[list.Count - 1], list[index]) = (list[index], swappedElement);
			list.RemoveAt(list.Count - 1);
		}
	}
}