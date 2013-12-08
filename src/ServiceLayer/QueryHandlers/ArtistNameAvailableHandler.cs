using System.Linq;
using Agatha.Common;
using MusicCompany.Common.Queries;
using MusicCompany.Common.ViewModel;
using MusicCompany.ServiceLayer.Base;
using NHibernate.Linq;

namespace MusicCompany.ServiceLayer.QueryHandlers
{
	public class ArtistNameAvailableHandler : QueryHandler<ArtistNameAvailableQuery, ArtistNameAvailableResponse>
	{
		public override Response Handle(ArtistNameAvailableQuery request)
		{
			var response = this.CreateTypedResponse();
			response.IsAvailable = this.Session.Linq<ArtistDetailView>()
				.Count(a => a.Name == request.PotentialName && a.Identifier != request.ExcludeIdentifier) == 0;
			return response;
		}
	}
}