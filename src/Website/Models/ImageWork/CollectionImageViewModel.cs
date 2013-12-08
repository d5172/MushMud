using MusicCompany.Common.ViewModel;
using PagedList;

namespace MusicCompany.Website.Models.ImageWork
{
	public class CollectionImageViewModel
	{
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

		public IPagedList<ImageSummaryView> Images
		{
			get;
			set;
		}
	}
}
