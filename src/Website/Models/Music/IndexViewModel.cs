using MusicCompany.Common.ViewModel;
using PagedList;

namespace MusicCompany.Website.Models.Music
{
	public class IndexViewModel
	{
		public IPagedList<TopLevelWorkSummaryView> Works
		{
			get;
			set;
		}
	}
}
