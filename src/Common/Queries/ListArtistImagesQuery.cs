using MusicCompany.Common.ViewModel;
using PagedList;

namespace MusicCompany.Common.Queries
{
	public class ListArtistImagesQuery : QueryRequestBase
	{
		public string ArtistIdentifier
		{
			get;
			set;
		}
	}

	public class ListArtistImagesQueryResponse : QueryResponseBase
	{
		public IPagedList<ImageSummaryView> Images
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
