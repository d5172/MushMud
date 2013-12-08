using MusicCompany.Common.ViewModel;
using PagedList;

namespace MusicCompany.Common.Queries
{
	public class ListTopLevelWorksByPersonQuery : QueryRequestBase
	{
		public string Username
		{
			get;
			set;
		}
	}

	public class ListTopLevelWorksByPersonQueryResponse : QueryResponseBase
	{
		public IPagedList<TopLevelWorkSummaryView> Works
		{
			get;
			set;
		}
	}
}
