using MusicCompany.Common.ViewModel;
using PagedList;

namespace MusicCompany.Website.Models.Person
{
	public class ProfileViewModel
	{
		public PersonDetailView Person
		{
			get;
			set;
		}

		public IPagedList<TopLevelWorkSummaryView> Works
		{
			get;
			set;
		}
	}
}
