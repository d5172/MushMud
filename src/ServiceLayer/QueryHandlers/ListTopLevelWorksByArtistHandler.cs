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
	public class ListTopLevelWorksByArtistHandler : QueryHandler<ListTopLevelWorksByArtistQuery, ListTopLevelWorksByArtistQueryResponse>
	{
		public override Response Handle(ListTopLevelWorksByArtistQuery request)
		{
			int total = this.Session.Linq<TopLevelWorkSummaryView>()
				.Where(w => w.ArtistIdentifier == request.ArtistIdentifier)
				.Count();

			var result = this.Session.Linq<TopLevelWorkSummaryView>()
				.Where(w => w.ArtistIdentifier == request.ArtistIdentifier)
				.Skip(request.Paging.GetSkipIndex())
				.Take(request.Paging.Size)
				.OrderByDescending(w => w.ReleaseDate)
				.ToList();

			var response = this.CreateTypedResponse();
			response.Works = new StaticPagedList<TopLevelWorkSummaryView>(result, request.Paging.ItemIndex(), request.Paging.Size, total);
			return response;
		}
	}
}