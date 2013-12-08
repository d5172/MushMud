using System.Web.Mvc;

namespace MusicCompany.Website
{
	public class AjaxEnabledAuthorizeAttribute : AuthorizeAttribute
	{
		public ActionResult LoginAction
		{
			get;
			set;
		}

		public AjaxEnabledAuthorizeAttribute(ActionResult loginAction) : base()
		{
			this.LoginAction = loginAction;
		}

		public override void OnAuthorization(AuthorizationContext filterContext)
		{
			if ( filterContext.RequestContext.HttpContext.Request.IsAjaxRequest() )
			{
				if ( !this.AuthorizeCore(filterContext.HttpContext) )
				{
					
				}
			}
			base.OnAuthorization(filterContext);
		}
	}
}