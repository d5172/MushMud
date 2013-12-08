using System.Linq;
using Agatha.Common;
using MusicCompany.Common.Queries;
using MusicCompany.Common.ViewModel;
using MusicCompany.ServiceLayer.Base;
using NHibernate.Linq;

namespace MusicCompany.ServiceLayer.QueryHandlers
{
	public class GetTrackDetailQueryHandler : QueryHandler<GetTrackDetailQuery, GetTrackDetailQueryResponse>
	{
		public override Response Handle(GetTrackDetailQuery request)
		{
			var response = this.CreateTypedResponse();
			response.Track = this.Session.Linq<AudioTrackSummaryView>().Single(t => t.CollectionIdentifier == request.CollectionIdentifier
				&& t.ArtistIdentifier == request.ArtistIdentifier
				&& t.Identifier == request.WorkIdentifier);

			return response;
		}
	}
}