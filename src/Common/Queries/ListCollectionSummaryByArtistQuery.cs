using MusicCompany.Common.ViewModel;
using PagedList;

namespace MusicCompany.Common.Queries
{
	public class ListCollectionSummaryByArtistQuery : QueryRequestBase
	{
		public string ArtistIdentifier
		{
			get;
			set;
		}
	}

	public class ListCollectionSummaryByArtistQueryResponse : QueryResponseBase
	{
		public IPagedList<CollectionSummaryView> Collections
		{
			get;
			set;
		}
	}
}
