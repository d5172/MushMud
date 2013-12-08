using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Context;
using Spring.Context.Support;

namespace MusicCompany.Infrastructure
{
	public static class Container
	{
		public static T GetObject<T>()
			where T : class
		{
			IApplicationContext context = ContextRegistry.GetContext();
			T instance = context.GetObject(typeof(T).Name) as T;
			return instance;
		}

		public static object GetObject(Type type)
		{
			IApplicationContext context = ContextRegistry.GetContext();
			string key = type.Name;
			return context.GetObject(key);
		}
	}
}
