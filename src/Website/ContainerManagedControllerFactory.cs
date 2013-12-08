using System;
using System.Web.Mvc;
using MusicCompany.Infrastructure;
using Agatha.Common.InversionOfControl;

namespace MusicCompany.Website.Infrastructure
{
	public class ContainerManagedControllerFactory : DefaultControllerFactory
	{
		protected override IController GetControllerInstance(Type controllerType)
		{
			if (controllerType == null)
			{
				return null;
			}
			return IoC.Container.Resolve(controllerType) as IController;
		}
	}
}