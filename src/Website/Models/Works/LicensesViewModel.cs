using System.Collections.Generic;
using MusicCompany.Common.ViewModel;

namespace MusicCompany.Website.Models.Works
{
	public class LicensesViewModel
	{
		public IEnumerable<LicenseDetailView> Licenses
		{
			get;
			set;
		}
	}
}
