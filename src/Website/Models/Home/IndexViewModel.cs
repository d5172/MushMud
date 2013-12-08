using MusicCompany.Common.ViewModel;
using PagedList;

namespace MusicCompany.Website.Models.Home
{
	public class IndexViewModel
	{

		public IPagedList<TopLevelWorkSummaryView> MostPopular
		{
			get;
			set;
		}

		public IPagedList<TopLevelWorkSummaryView> NewReleases
		{
			get;
			set;
		}
	}
}
