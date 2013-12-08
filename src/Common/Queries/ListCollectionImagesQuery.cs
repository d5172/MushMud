using MusicCompany.Common.ViewModel;
using PagedList;

namespace MusicCompany.Common.Queries
{
	public class ListCollectionImagesQuery : QueryRequestBase
	{
		public string ArtistIdentifier
		{
			get;
			set;
		}

		public string CollectionIdentifier
		{
			get;
			set;
		}
	}

	public class ListCollectionImagesQueryResponse : QueryResponseBase
	{
		public IPagedList<ImageSummaryView> Images
		{
			get;
			set;
		}

		public string CollectionTitle
		{
			get;
			set;
		}

		public string ArtistName
		{
			get;
			set;
		}
	}
}
