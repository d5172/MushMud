using System.Linq;
using Agatha.Common;
using MusicCompany.Common.Queries;
using MusicCompany.Common.ViewModel;
using MusicCompany.ServiceLayer.Base;
using NHibernate.Linq;

namespace MusicCompany.ServiceLayer.QueryHandlers
{
	public class GetCollectionDetailQueryHandler : QueryHandler<GetCollectionDetailQuery, GetCollectionDetailQueryResponse>
	{
		public override Response Handle(GetCollectionDetailQuery request)
		{
			var response = this.CreateTypedResponse();
			response.Collection = this.Session.Linq<CollectionSummaryView>()
				.Single(c => c.ArtistIdentifier == request.ArtistIdentifier
					&& c.Identifier == request.CollectionIdentifier);
			return response;
		}
	}
}