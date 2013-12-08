using System;
using System.Web.Mvc;
using MusicCompany.Common.Queries;
using MusicCompany.Website.Models.Charts;

namespace MusicCompany.Website.Controllers
{
    public class ChartsController : ExtendedController
    {
		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Index()
		{
			try
			{
				var query = new ListMostPopularWorksQuery();
				query.Paging.Number = 1;
				query.Paging.Size = 100;

				var response = this.ProcessRequest<ListMostPopularWorksResponse>(query);
				var model = new IndexViewModel();
				model.Works = response.Works;

				return this.GetResult(View(model), null);
			}
			catch ( Exception ex )
			{
				return this.GetFailure(ex, this.GoHome());
			}
		}

    }
}
