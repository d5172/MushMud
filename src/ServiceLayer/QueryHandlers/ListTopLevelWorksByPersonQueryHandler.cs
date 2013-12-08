using System.Collections.Generic;
using System.Linq;
using Agatha.Common;
using MusicCompany.Common.Queries;
using MusicCompany.Common.ViewModel;
using MusicCompany.ServiceLayer.Base;
using NHibernate.Linq;
using PagedList;

namespace MusicCompany.ServiceLayer.QueryHandlers
{
	public class ListTopLevelWorksByPersonQueryHandlerHandler : QueryHandler<ListTopLevelWorksByPersonQuery, ListTopLevelWorksByPersonQueryResponse>
	{
		public override Response Handle(ListTopLevelWorksByPersonQuery request)
		{
			var artistIds = this.Session.Linq<ArtistPersonSummaryView>()
				.Where(ap => ap.Username == request.Username)
				.Select(ap => ap.Identifier)
				.ToArray();

			int total = this.Session.Linq<TopLevelWorkSummaryView>()
			.Where(w => artistIds.Contains(w.ArtistIdentifier))
			.Count();

			IEnumerable<TopLevelWorkSummaryView> result;
			if ( total > 0 )
			{
				result = this.Session.Linq<TopLevelWorkSummaryView>()
					.Where(w => artistIds.Contains(w.ArtistIdentifier))
					.Skip(request.Paging.GetSkipIndex())
					.Take(request.Paging.Size)
					.OrderByDescending(w => w.ReleaseDate)
					.ToList();
			}
			else
			{
				result = new List<TopLevelWorkSummaryView>();
			}

			var response = this.CreateTypedResponse();
			response.Works = new StaticPagedList<TopLevelWorkSummaryView>(result, request.Paging.ItemIndex(), request.Paging.Size, total);
			return response;
		}
	}
}