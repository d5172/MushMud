using System;
using Agatha.Common.InversionOfControl;
using Spring.Context;
using Spring.Context.Support;
using Spring.Objects.Factory.Config;
using Spring.Objects.Factory.Support;
using System.Collections.Generic;
using System.Linq;

namespace MusicCompany.ServiceLayer.Support
{
	/// <summary>
	/// Spring.NET Implementation of Agatha.Common.InversionOfControl.IContainer.
	/// When an Agatha Configuration initializes, it registers various objects here,
	/// including all RequestHandlers it finds in the ServiceLayer assembly.
	/// This implementation is desinged to be used in conjunction with traditional
	/// XML based configuration of spring objects, so when asked to register an object
	/// it checks to see if it already has it.  In this way we can configure certain 
	/// request handlers with interceptors as needed and still have Agatha wire up the rest.
	/// </summary>
	public class SpringContainer : IContainer
	{

		private static readonly IDictionary<string, string> keyNameMap = new Dictionary<string, string>();

		private IConfigurableApplicationContext GetContext()
		{
			return (IConfigurableApplicationContext) ContextRegistry.GetContext();
		}

		private string GetPreferredRegistrationKey(Type type)
		{
			return type.FullName;
		}

		private string GetAlternateRegistrationKey(Type type)
		{
			return type.Name;
		}

		private string GetResolutionKey(Type type)
		{
			string preferredKey = this.GetPreferredRegistrationKey(type);
			if ( this.GetContext().ContainsObject(preferredKey) )
			{
				return preferredKey;
			}
			else if ( keyNameMap.ContainsKey(preferredKey) )
			{
				return keyNameMap[preferredKey];
			}
			else
			{
				return GetAlternateRegistrationKey(type);
			}
		}

		#region IContainer Members

		public void Register<TComponent, TImplementation>(Lifestyle lifestyle)
		{
			Register(typeof(TComponent), typeof(TImplementation), lifestyle);
		}

		public void Register(Type componentType, Type implementationType, Lifestyle lifeStyle)
		{
			string key = this.GetPreferredRegistrationKey(componentType);
			IConfigurableApplicationContext context = this.GetContext();
			if ( !context.ContainsObject(key) )
			{
				string existingName = context.GetObjectNamesForType(componentType).SingleOrDefault();
				if ( !string.IsNullOrEmpty(existingName) )
				{
					keyNameMap.Add(key, existingName);
				}
				else
				{
					IObjectDefinitionFactory objectDefinitionFactory = new DefaultObjectDefinitionFactory();
					ObjectDefinitionBuilder builder = ObjectDefinitionBuilder.RootObjectDefinition(objectDefinitionFactory, implementationType);
					builder.SetAutowireMode(AutoWiringMode.AutoDetect);
					builder.SetDependencyCheck(DependencyCheckingMode.None);
					builder.SetSingleton(lifeStyle == Lifestyle.Singleton);
					context.ObjectFactory.RegisterObjectDefinition(key, builder.ObjectDefinition);
				}
			}
		}

		public void RegisterInstance<TComponent>(TComponent instance)
		{
			RegisterInstance(typeof(TComponent), instance);
		}

		public void RegisterInstance(Type componentType, object instance)
		{
			string key = this.GetPreferredRegistrationKey(componentType);
			if ( !this.GetContext().ContainsObject(key) )
			{
				this.GetContext().ObjectFactory.RegisterSingleton(key, instance);
			}
		}

		public void Release(object component)
		{
			var disposable = component as IDisposable;
			if ( disposable != null )
			{
				disposable.Dispose();
			}
		}

		public object Resolve(Type componentType)
		{
			IConfigurableApplicationContext context = this.GetContext();
			string key = this.GetResolutionKey(componentType);
			var component = context.GetObject(key);
			return component;
		}

		public TComponent Resolve<TComponent>()
		{
			return (TComponent) Resolve(typeof(TComponent));
		}

		#endregion
	}
}
