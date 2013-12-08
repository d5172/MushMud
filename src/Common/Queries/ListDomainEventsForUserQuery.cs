using MusicCompany.Common.ViewModel;
using PagedList;

namespace MusicCompany.Common.Queries
{
	public class ListDomainEventsForUserQuery : QueryRequestBase
	{
		public string Username
		{
			get;
			set;
		}
	}

	public class ListDomainEventsForUserQueryResponse : QueryResponseBase
	{
		public IPagedList<DomainEventView> Events
		{
			get;
			set;
		}
	}
}
