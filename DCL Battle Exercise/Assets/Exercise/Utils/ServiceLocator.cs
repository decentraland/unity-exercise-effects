using System;
using System.Collections.Generic;

namespace Exercise.Utils
{
	public interface IService
	{
	}

	public class ServiceLocator
	{
		public static ServiceLocator Instance;

		private readonly Dictionary<Type, IService> _services = new Dictionary<Type, IService>();

		public void Register<T>(T service) where T : IService
		{
			_services[typeof(T)] = service;
		}

		public T GetService<T>() where T : IService
		{
			return (T) _services[typeof(T)];
		}
	}
}