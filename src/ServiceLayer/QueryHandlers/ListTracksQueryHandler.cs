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
	public class ListTracksQueryHandler : QueryHandler<ListTracksQuery, ListTracksQueryResponse>
	{
		public override Response Handle(ListTracksQuery request)
		{
			var response = this.CreateTypedResponse();
			response.Tracks = this.Session.Linq<AudioTrackSummaryView>()
				.Where(a => a.ArtistIdentifier == request.ArtistIdentifier && a.CollectionIdentifier == request.CollectionIdentifier)
				.OrderBy(a => a.ViewOrder)
				.ToList();

			return response;
		}
	}
}