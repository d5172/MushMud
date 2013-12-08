using Agatha.Common;
using MusicCompany.Common.Queries;
using MusicCompany.Common.ViewModel;
using MusicCompany.ServiceLayer.Base;
using System.Linq;
using NHibernate.Linq;

namespace MusicCompany.ServiceLayer.QueryHandlers
{
	public class GetArtistDetailQueryHandler : QueryHandler<GetArtistDetailQuery, GetArtistDetailResponse>
	{
		public override Response Handle(GetArtistDetailQuery request)
		{
			var artist = this.Session.Get<ArtistDetailView>(request.Identifier);
			var response = this.CreateTypedResponse();
			response.Artist = artist;
			return response;
		}
	}
}