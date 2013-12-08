using MusicCompany.Common.ViewModel;
using PagedList;

namespace MusicCompany.Common.Queries
{
	public class WorkSearchQuery : QueryRequestBase
	{
		public string[] SearchTerms
		{
			get;
			set;
		}
	}

	public class WorkSearchQueryResponse : QueryResponseBase
	{
		public IPagedList<TopLevelWorkSummaryView> Works
		{
			get;
			set;
		}
	}
}
