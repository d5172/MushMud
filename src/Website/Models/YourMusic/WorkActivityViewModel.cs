using MusicCompany.Common.ViewModel;
using PagedList;

namespace MusicCompany.Website.Models.YourMusic
{
	public class WorkActivityViewModel
	{
		public IPagedList<DomainEventView> Events
		{
			get;
			set;
		}
	}
}
