using System.Linq;
using Agatha.Common;
using MusicCompany.Common.Queries;
using MusicCompany.Common.ViewModel;
using MusicCompany.ServiceLayer.Base;
using NHibernate.Linq;
using PagedList;

namespace MusicCompany.ServiceLayer.QueryHandlers
{
	public class ListCollectionSummaryByArtistHandler : QueryHandler<ListCollectionSummaryByArtistQuery, ListCollectionSummaryByArtistQueryResponse>
	{
		public override Response Handle(ListCollectionSummaryByArtistQuery request)
		{
			var total = this.Session.Linq<CollectionSummaryView>()
				.Where(c => c.ArtistIdentifier == request.ArtistIdentifier)
				.Count();

			var result = this.Session.Linq<CollectionSummaryView>()
				.Where(c => c.ArtistIdentifier == request.ArtistIdentifier)
				.OrderBy(c => c.Title)
				.Skip(request.Paging.GetSkipIndex())
				.Take(request.Paging.Size)
				.ToList();

			foreach ( var collection in result )
			{
				collection.Tracks = this.Session.Linq<AudioTrackSummaryView>().Where(t => t.CollectionIdentifier == collection.Identifier).OrderBy(t => t.ViewOrder).ToList();
			}
			
			var response = this.CreateTypedResponse();
			response.Collections = new StaticPagedList<CollectionSummaryView>(result, request.Paging.ItemIndex(), request.Paging.Size, total);

			return response;
		}
	}
}