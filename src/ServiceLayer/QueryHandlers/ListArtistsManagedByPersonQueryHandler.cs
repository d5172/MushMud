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
	public class ListArtistsManagedByPersonQueryHandler : QueryHandler<ListArtistsManagedByPersonQuery, ListArtistsManagedByPersonQueryResponse>
	{
		public override Response Handle(ListArtistsManagedByPersonQuery request)
		{
			var countQuery = this.Session.Linq<ArtistPersonSummaryView>()
				.Where(a => a.Username == request.Username);
			int total = countQuery.Count();

			var resultQuery = this.Session.Linq<ArtistPersonSummaryView>()
				.Where(a => a.Username == request.Username)
				.OrderBy(a => a.Name)
				.Take(request.Paging.Size)
				.Skip(request.Paging.GetSkipIndex())
				.ToList();

			var response = this.CreateTypedResponse();
			response.Artists = new StaticPagedList<ArtistPersonSummaryView>(resultQuery, request.Paging.ItemIndex(), request.Paging.Size, total);

			return response;


		}
	}
}