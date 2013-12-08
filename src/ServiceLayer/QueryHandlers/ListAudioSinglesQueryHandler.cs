using System.Linq;
using Agatha.Common;
using MusicCompany.Common.Queries;
using MusicCompany.Common.ViewModel;
using MusicCompany.ServiceLayer.Base;
using NHibernate.Linq;
using PagedList;

namespace MusicCompany.ServiceLayer.QueryHandlers
{
	public class ListAudioSinglesQueryHandler : QueryHandler<ListAudioSinglesQuery, ListAudioSinglesQueryResponse>
	{
		public override Response Handle(ListAudioSinglesQuery request)
		{
			var total = this.Session.Linq<AudioSingleSummaryView>()
				.Where(a => a.ArtistIdentifier == request.ArtistIdentifier)
				.Count();

			var result = this.Session.Linq<AudioSingleSummaryView>()
				.Where(a => a.ArtistIdentifier == request.ArtistIdentifier)
				.OrderBy(a => a.ReleaseDate)
				.Skip(request.Paging.GetSkipIndex())
				.Take(request.Paging.Size)
				.ToList();

			var response = this.CreateTypedResponse();
			response.Singles = new StaticPagedList<AudioSingleSummaryView>(result, request.Paging.ItemIndex(), request.Paging.Size, total);

			return response;
		}
	}
}