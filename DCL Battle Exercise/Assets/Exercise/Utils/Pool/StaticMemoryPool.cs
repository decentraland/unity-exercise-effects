using System.Collections.Generic;
using System.Linq;
using UnityEngine.Assertions;

namespace Exercise.Utils.Pool
{
	public abstract class StaticMemoryPool<TPool, T> where T : class, new() where TPool : StaticMemoryPool<TPool, T>, new()
	{
		private static readonly TPool Instance = new TPool();

		private readonly Stack<T> _stack = new Stack<T>();

		protected StaticMemoryPool()
		{
			Assert.IsTrue(typeof(TPool) == GetType());
		}

		public static T Rent()
		{
			return Instance.RentInternal();
		}

		public static void Return(T obj)
		{
			Instance.ReturnInternal(obj);
		}

		public void ReturnInternal(T @object)
		{
			Assert.IsTrue(_stack.All(o => !ReferenceEquals(o, @object)));
			OnReturn(@object);
			_stack.Push(@object);
		}

		protected virtual void OnReturn(T @object)
		{
		}

		private T RentInternal()
		{
			return _stack.Count > 0 ? _stack.Pop() : new T();
		}
	}
}