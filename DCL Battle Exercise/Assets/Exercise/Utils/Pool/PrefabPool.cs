using System.Collections.Generic;
using UnityEngine;

namespace Exercise.Utils.Pool
{
	public class PrefabPool
	{
		/// <summary>
		///     Disabled root where objects are piled up while they are not used
		/// </summary>
		
		private readonly Transform _poolRoot;

		public PrefabPool(Transform poolRoot)
		{
			_poolRoot = poolRoot;
		}

		private readonly Dictionary<GameObject, Stack<GameObject>> _pools = new Dictionary<GameObject, Stack<GameObject>>();
		
		private readonly Dictionary<GameObject, Stack<GameObject>> _rentedInstances =
			new Dictionary<GameObject, Stack<GameObject>>();

		public T Get<T>(Transform parent, T prefab) where T : MonoBehaviour
		{
			var go = prefab.gameObject;
			
			if (_pools.TryGetValue(go, out var pool))
			{
				if (pool.Count > 0)
				{
					var instance = pool.Pop();
					instance.transform.SetParent(parent, false);
					return RegisterRentedInstance<T>(pool, instance);
				}
			}
			else
			{
				_pools[go] = pool = new Stack<GameObject>();
			}
			
			
			// Create a new instance
			return RegisterRentedInstance<T>(pool, Object.Instantiate(prefab.gameObject, parent));
		}

		private T RegisterRentedInstance<T>(Stack<GameObject> pool, GameObject instance) where T : MonoBehaviour
		{
			instance.SetActive(true);
			_rentedInstances[instance] = pool;
			return instance.GetComponent<T>();
		}

		public void Release<T>(T instance) where T : MonoBehaviour
		{
			var go = instance.gameObject;
			
			if (!_rentedInstances.TryGetValue(go, out var pool))
				return;

			_rentedInstances.Remove(go);
			pool.Push(go);
			go.SetActive(false);
			go.transform.SetParent(_poolRoot);
		}
	}
}