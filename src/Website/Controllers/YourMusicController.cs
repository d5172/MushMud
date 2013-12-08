using System;
using System.Web.Mvc;
using MusicCompany.Common.Queries;
using MusicCompany.Website.Models.YourMusic;

namespace MusicCompany.Website.Controllers
{
	[Authorize]
	public class YourMusicController : ExtendedController
	{
		public int WorkActivityPageSize
		{
			get;
			set;
		}
	
		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Index()
		{
			var model = new IndexViewModel();
			try
			{
				var query = new ListArtistsManagedByPersonQuery();
				query.Username = this.HttpContext.User.Identity.Name;
				model.Artists = this.ProcessRequest<ListArtistsManagedByPersonQueryResponse>(query).Artists;
				return GetResult(View(model), null);
			}
			catch (Exception ex)
			{
				return GetFailure(ex, View(model));
			}
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult WorkActivity(int? page)
		{
			var model = new WorkActivityViewModel();
			try
			{
				var query = new ListDomainEventsForUserQuery();
				query.Username = this.User.Identity.Name;
				query.Paging.Size = this.WorkActivityPageSize;
				query.Paging.Number = page ?? 1;
				var response = this.ProcessRequest<ListDomainEventsForUserQueryResponse>(query);
				model.Events = response.Events;
				return GetResult(View(model), View("WorkActivityList", model));
			}
			catch ( Exception ex )
			{
				return GetFailure(ex, this.RedirectToAction("Index"));
			}
		}
	}
}