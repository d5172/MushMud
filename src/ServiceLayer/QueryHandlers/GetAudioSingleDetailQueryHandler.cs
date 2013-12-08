using System.Linq;
using Agatha.Common;
using MusicCompany.Common.Queries;
using MusicCompany.Common.ViewModel;
using MusicCompany.ServiceLayer.Base;
using NHibernate.Linq;

namespace MusicCompany.ServiceLayer.QueryHandlers
{
	public class GetAudioSingleDetailQueryHandler : QueryHandler<GetAudioSingleDetailQuery, GetAudioSingleDetailQueryResponse>
	{
		public override Response Handle(GetAudioSingleDetailQuery request)
		{
			var response = this.CreateTypedResponse();
			response.Single = this.Session.Linq<AudioSingleSummaryView>().Single(
				t => t.ArtistIdentifier == request.ArtistIdentifier
					&& t.Identifier == request.WorkIdentifier);

			return response;

		}
	}
}