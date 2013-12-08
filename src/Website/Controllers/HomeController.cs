using System.Web.Mvc;
using MusicCompany.Common.Queries;
using MusicCompany.Website.Models.Home;

namespace MusicCompany.Website.Controllers
{
	[HandleError]
	public class HomeController : ExtendedController
	{
		public int MostPopularCount
		{
			get;
			set;
		}

		public int NewReleasesCount
		{
			get;
			set;
		}

		[HandleError]
		public ActionResult Index()
		{
			IndexViewModel model = new IndexViewModel();

			ListMostPopularWorksQuery mostPopularQuery = new ListMostPopularWorksQuery();
			mostPopularQuery.Paging.Size = this.MostPopularCount;
			var mostPopularResponse = this.ProcessRequest<ListMostPopularWorksResponse>(mostPopularQuery);
			model.MostPopular = mostPopularResponse.Works;

			ListNewReleasesQuery newReleasesQuery = new ListNewReleasesQuery();
			newReleasesQuery.Paging.Size = this.NewReleasesCount;
			var newReleasesResponse = this.ProcessRequest<ListNewReleasesResponse>(newReleasesQuery);
			model.NewReleases = newReleasesResponse.Works;

			return View(model);
		}

		public ActionResult TestPage()
		{
			return View();
		}
	}
}
