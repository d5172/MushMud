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
	public class ListNewReleasesHandler : QueryHandler<ListNewReleasesQuery, ListNewReleasesResponse>
	{
		public override Response Handle(ListNewReleasesQuery request)
		{
			var result = this.Session.Linq<TopLevelWorkSummaryView>()
				.Where(w => w.IsReleased)
				.Skip(request.Paging.GetSkipIndex())
				.Take(request.Paging.Size)
				.OrderByDescending(w => w.ReleaseDate)
				.ToList();
			var response = this.CreateTypedResponse();
			response.Works = new StaticPagedList<TopLevelWorkSummaryView>(result, request.Paging.ItemIndex(), request.Paging.Size, int.MaxValue);
			return response;
		}
	}
}