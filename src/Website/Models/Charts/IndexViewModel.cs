using System.Collections.Generic;
using MusicCompany.Common.ViewModel;

namespace MusicCompany.Website.Models.Charts
{
	public class IndexViewModel
	{
		public IEnumerable<TopLevelWorkSummaryView> Works
		{
			get;
			set;
		}
	}
}
