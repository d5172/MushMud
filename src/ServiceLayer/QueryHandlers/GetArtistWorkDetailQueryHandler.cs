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
	public class GetArtistWorkDetailQueryHandler : QueryHandler<GetArtistWorkDetailQuery, GetArtistWorkDetailQueryResponse>
	{
		public override Response Handle(GetArtistWorkDetailQuery request)
		{
			var artist = this.Session.Get<ArtistWorkDetailView>(request.Identifier);
			artist.Collections = this.Session.Linq<CollectionSummaryView>().Where(c => c.ArtistIdentifier == request.Identifier).OrderBy(c => c.Title).ToList();
			foreach ( var collection in artist.Collections )
			{
				collection.Tracks = this.Session.Linq<AudioTrackSummaryView>().Where(t => t.CollectionIdentifier == collection.Identifier).OrderBy(t => t.ViewOrder).ToList();
			}
			artist.Singles = this.Session.Linq<AudioSingleSummaryView>().Where(s => s.ArtistIdentifier == request.Identifier).OrderBy(s => s.Title).ToList();
			var response = this.CreateTypedResponse();
			response.Artist = artist;
			return response;
		}
	}
}