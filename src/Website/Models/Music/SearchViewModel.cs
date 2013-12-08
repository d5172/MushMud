using MusicCompany.Common.ViewModel;
using PagedList;

namespace MusicCompany.Website.Models.Music
{
	public class SearchViewModel
	{
		public IPagedList<TopLevelWorkSummaryView> List
		{
			get;
			set;
		}

		public string Terms
		{
			get;
			set;
		}
	}
}
