using System.Linq;
using Agatha.Common;
using MusicCompany.Common.Queries;
using MusicCompany.Common.ViewModel;
using MusicCompany.ServiceLayer.Base;
using NHibernate.Linq;
using PagedList;

namespace MusicCompany.ServiceLayer.QueryHandlers
{
	public class ListArtistsQueryHandler : QueryHandler<ListArtistsQuery, ListArtistsResponse>
	{
		public override Response Handle(ListArtistsQuery request)
		{
			var queryable = this.Session.Linq<ArtistSummaryView>();
			int total = queryable.Count();

			var result = this.Session.Linq<ArtistSummaryView>()
				.OrderBy(a => a.Name)
				.Take(request.Paging.Size)
				.Skip(request.Paging.GetSkipIndex())
				.ToList();
			
			var response = this.CreateTypedResponse();
			response.Artists = new StaticPagedList<ArtistSummaryView>(result, request.Paging.ItemIndex(), request.Paging.Size, total);

			return response;

		}
	}
}