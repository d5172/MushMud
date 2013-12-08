using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MusicCompany.Website.Infrastructure;
using System.Web.Security;
using MusicCompany.ServiceLayer.Support;

namespace MusicCompany.Website
{
	// Note: For instructions on enabling IIS6 or IIS7 classic mode, 
	// visit http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
			routes.IgnoreRoute("Gateway.aspx");
			routes.IgnoreRoute("apps");
			routes.IgnoreRoute("WEB-INF");
			routes.IgnoreRoute("favicon.ico");
			routes.IgnoreRoute("robots.txt");

			

			routes.MapRoute(
				"",
				"Image/FullSize/{id}/{title}",
				new
				{
					controller = "Image",
					action = "FullSize"
				}
			);
			
			routes.MapRoute(
				"",
				"Image/{action}/{id}/{width}",
				new
				{
					controller = "Image",
					action = "Index"
				}
			);

			routes.MapRoute(
			    "",
			    "Stream/{action}/{artistId}/{workId}/{collectionId}/{format}",
			    new
			    {
			        controller="Stream",
			        action = "Play"
			    }
			);

			routes.MapRoute(
			    "",
			    "Stream/{action}/{artistId}/{workId}/{format}",
			    new
			    {
					controller="Stream",
			        action = "Play"
			    }
			);

			routes.MapRoute(
				"",
				"Artist",
				new
				{
					controller = "Artist",
					action = "Index",
					page = 1
				}
			);

			routes.MapRoute(
				"",
				"Artist/Page{page}",
				new
				{
					controller = "Artist",
					action = "Index"
				},
				new
				{
					page = @"\d+"
				}
			);

			routes.MapRoute(
				"",
				"Music",
				new
				{
					controller = "Music",
					action = "Index",
					page = 1
				}
			);

			routes.MapRoute(
				"",
				"Music/Page{page}",
				new
				{
					controller = "Music",
					action = "Index"
				},
				new
				{
					page = @"\d+"
				}
			);

			routes.MapRoute(
				"",
				"Comments/{id}",
				new
				{
					controller = "Comments",
					action = "Index"
				}
			);

			routes.MapRoute(
				"",
				"Comments/Add/{workId}",
				new
				{
					controller = "Comments",
					action = "Add"
				}
			);

			routes.MapRoute(
				"",
				"Person/Profile/{username}",
				new
				{
					controller = "Person",
					action = "Profile",
					page = 1
				}
			);

			routes.MapRoute(
				"",
				"Person/Profile/{username}/Page{page}",
				new
				{
					controller = "Person",
					action = "Profile"
				},
				new
				{
					page = @"\d+"
				}
			);

			routes.MapRoute(
				"Default",
				"{controller}/{action}/{artistId}/{workId}/{collectionId}",
				new
				{
					controller = "Home",
					action = "Index",
					artistId = "",
					workId = "",
					collectionId = ""
				}
			);
		}

		protected void Application_Start()
		{
			//Register our routes (duh)
			RegisterRoutes(RouteTable.Routes);

			//bootstrap the agatha stuff
			SingleProcessConfiguration.Initialize();

			//Set the controller factory to our container-managed factory
			ControllerBuilder.Current.SetControllerFactory(new ContainerManagedControllerFactory());

#if DEBUG
			// inti the NHibernate profiler when in debug mode
			HibernatingRhinos.NHibernate.Profiler.Appender.NHibernateProfiler.Initialize();
#endif
		}

		public override void Init()
		{
			base.Init();
			this.BeginRequest += new EventHandler(MvcApplication_BeginRequest);
		}

		void MvcApplication_BeginRequest(object sender, EventArgs e)
		{
			/* Fix for the Flash Player Cookie bug in Non-IE browsers.
			 * Since Flash Player always sends the IE cookies even in FireFox
			 * we have to bypass the cookies by sending the values as part of the POST or GET
			 * and overwrite the cookies with the passed in values.
			 * 
			 * The theory is that at this point (BeginRequest) the cookies have not been read by
			 * the Session and Authentication logic and if we update the cookies here we'll get our
			 * Session and Authentication restored correctly
			 */
			try
			{
				string auth_param_name = "AUTHID";
				string auth_cookie_name = FormsAuthentication.FormsCookieName;

				if (HttpContext.Current.Request.Form[auth_param_name] != null)
				{
					UpdateCookie(auth_cookie_name, HttpContext.Current.Request.Form[auth_param_name]);
				}
				else if (HttpContext.Current.Request.QueryString[auth_param_name] != null)
				{
					UpdateCookie(auth_cookie_name, HttpContext.Current.Request.QueryString[auth_param_name]);
				}

			}
			catch (Exception)
			{
				Response.StatusCode = 500;
				Response.Write("Error Initializing Forms Authentication");
			}
		}

		void UpdateCookie(string cookie_name, string cookie_value)
		{
			HttpCookie cookie = HttpContext.Current.Request.Cookies.Get(cookie_name);
			if (cookie == null)
			{
				cookie = new HttpCookie(cookie_name);
				HttpContext.Current.Request.Cookies.Add(cookie);
			}
			cookie.Value = cookie_value;
			HttpContext.Current.Request.Cookies.Set(cookie);
		}
	}
}