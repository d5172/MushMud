using MusicCompany.Common.ViewModel;
using PagedList;

namespace MusicCompany.Common.Queries
{
	public class ListTopLevelWorksByArtistQuery : QueryRequestBase
	{
		public string ArtistIdentifier
		{
			get;
			set;
		}
	}

	public class ListTopLevelWorksByArtistQueryResponse : QueryResponseBase
	{
		public IPagedList<TopLevelWorkSummaryView> Works
		{
			get;
			set;
		}
	}
}
