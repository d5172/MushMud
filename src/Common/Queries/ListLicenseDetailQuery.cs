using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagedList;
using MusicCompany.Common.ViewModel;

namespace MusicCompany.Common.Queries
{
	public class ListLicenseDetailQuery : QueryRequestBase
	{
	}

	public class ListLicenseDetailQueryResponse : QueryResponseBase
	{
		public IEnumerable<LicenseDetailView> Licenses
		{
			get;
			set;
		}
	}
}
