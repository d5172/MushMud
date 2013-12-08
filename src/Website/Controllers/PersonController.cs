using System;
using System.Web.Mvc;
using MusicCompany.Common.Queries;
using MusicCompany.Website.Models.Person;

namespace MusicCompany.Website.Controllers
{
    public class PersonController : ExtendedController
    {
		public int WorksPageSize
		{
			get;
			set;
		}

		[AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Profile(string username, int? page)
		{
			try
			{
				var personQuery = new GetPersonDetailQuery();
				personQuery.Username = username;
				var personResponse = this.ProcessRequest<GetPersonDetailQueryResponse>(personQuery);
				var model = new ProfileViewModel();
				model.Person = personResponse.Person;

				var worksQuery = new ListTopLevelWorksByPersonQuery();
				worksQuery.Username = username;
				worksQuery.Paging.Number = page ?? 1;
				worksQuery.Paging.Size = this.WorksPageSize;
				var worksResponse = this.ProcessRequest<ListTopLevelWorksByPersonQueryResponse>(worksQuery);
				model.Works = worksResponse.Works;

				return GetResult(View(model), null);
			}
			catch ( Exception ex )
			{
				return GetFailure(ex, this.GoHome());
			}
        }

    }
}
