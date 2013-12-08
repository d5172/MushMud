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
	public class ListDomainEventsForUserHandler : QueryHandler<ListDomainEventsForUserQuery, ListDomainEventsForUserQueryResponse>
	{
		public override Response Handle(ListDomainEventsForUserQuery request)
		{
			int count = this.Session.Linq<DomainEventView>()
				.Count(d => d.Username == request.Username || d.EventUsername == request.Username);

			IList<DomainEventView> result = this.Session.Linq<DomainEventView>()
				.Where(d => d.Username == request.Username || d.EventUsername == request.Username)
				.OrderByDescending(d => d.EventDate)
				.Skip(request.Paging.GetSkipIndex())
				.Take(request.Paging.Size)
				.ToList();

			var response = this.CreateTypedResponse();
			response.Events = new StaticPagedList<DomainEventView>(result, request.Paging.ItemIndex(), request.Paging.Size, count);
			return response;
		}
	}
}