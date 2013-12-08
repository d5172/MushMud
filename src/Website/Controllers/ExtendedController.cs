using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Agatha.Common;
using Agatha.Common.InversionOfControl;
using Elmah;
using MusicCompany.Common.Commands;
using MusicCompany.Common.Queries;
using MusicCompany.Common.ViewModel;
using MusicCompany.Website.Models;

namespace MusicCompany.Website.Controllers
{
	public class ExtendedController : Controller
	{

		#region RequestDispatcherAwareController

		public IRequestDispatcher RequestDispatcher
		{
			get
			{
				return IoC.Container.Resolve<IRequestDispatcher>();
			}
		}

		protected virtual ActionResult GoHome()
		{
			return RedirectToAction("Index", "Home");
		}

		protected virtual void CheckForException(Response response)
		{
			if ( response.ExceptionType != ExceptionType.None )
			{
				throw new Exception(response.Exception.Message + Environment.NewLine + response.Exception.StackTrace);
			}
		}

		protected virtual T ProcessRequest<T>(Request request)
			where T : Response
		{
			CommandRequestBase command = request as CommandRequestBase;
			if ( command != null )
			{
				if ( User.Identity.IsAuthenticated )
				{
					command.CommandContext = new CommandContext(User.Identity.Name);
				}
			}
			T response = this.RequestDispatcher.Get<T>(request);
			this.CheckForException(response);
			return response;
		}

		protected override void OnActionExecuted(ActionExecutedContext filterContext)
		{
			base.OnActionExecuted(filterContext);
			if ( RequestDispatcher != null )
			{
				IoC.Container.Release(RequestDispatcher);
			}
		}

		#endregion

		protected IEnumerable<LicenseSummaryView> GetLicenseSummaries()
		{
			var licenseQuery = new ListLicenseSummariesQuery();
			var licenseQueryResponse = this.ProcessRequest<ListLicenseSummariesQueryResponse>(licenseQuery);
			return licenseQueryResponse.LicenseSummaries;
		}

		protected ActionResult GetResult(ActionResult synchronousResult, ActionResult ajaxResult)
		{
			if (Request.IsAjaxRequest())
			{
				if (ajaxResult == null)
				{
					throw new Exception("action does not support ajax result");
				}
				return ajaxResult;
			}
			else
			{
				if (synchronousResult == null)
				{
					throw new Exception("action does not support synchronous result");
				}
				return synchronousResult;
			}
		}

		protected ActionResult GetFailure(Exception ex, ActionResult synchronousResult)
		{
			UserMessage userMessage = this.GetExceptionUserMessage(ex);
			if (Request.IsAjaxRequest())
			{
				return View("UserMessagePartial", userMessage);
			}
			else
			{
				if (synchronousResult == null)
				{
					throw new Exception("action does not support synchronous result");
				}
				this.AddUserMessage(userMessage);
				return synchronousResult;
			}
		}

		protected ActionResult PostFailure(Exception ex, ActionResult synchronousResult)
		{
			UserMessage userMessage = this.GetExceptionUserMessage(ex);
			return this.PostResult(userMessage, synchronousResult);
		}

		protected UserMessage GetExceptionUserMessage(Exception ex)
		{
			string message;
			UserMessageType type;
			if (typeof(System.ApplicationException).IsAssignableFrom(ex.GetType()))
			{
				type = UserMessageType.Warning;
#if DEBUG
				message = ex.ToString();
#else
				message = ex.Message;
#endif
			}
			else
			{
				type = UserMessageType.Error;
#if DEBUG
				message = ex.ToString();
#else
				message = "An error occurred.  Please try again.";
				ErrorSignal.FromCurrentContext().Raise(ex);
#endif
			}
			UserMessage userMessage = new UserMessage(message, type);
			return userMessage;
		}

		protected ActionResult PostResult(UserMessage message, ActionResult synchronousResult)
		{
			if (Request.IsAjaxRequest())
			{
				return Json(message);
			}
			else
			{
				this.AddUserMessage(message);
				if (synchronousResult == null)
				{
					throw new Exception("action does not support synchronous result");
				}
				return synchronousResult;
			}
		}

		protected void AddUserMessage(string message, UserMessageType type)
		{
			UserMessage userMessage = new UserMessage(message, type);
			AddUserMessage(userMessage);
		}

		protected void AddUserMessage(UserMessage userMessage)
		{
			this.Session.Add("UserMessage", userMessage);
		}
	}
}