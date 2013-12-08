using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagedList;
using MusicCompany.Common.ViewModel;

namespace MusicCompany.Common.Queries
{
	public class ListLicenseSummariesQuery : QueryRequestBase
	{
	}

	public class ListLicenseSummariesQueryResponse : QueryResponseBase
	{
		public IEnumerable<LicenseSummaryView> LicenseSummaries
		{
			get;
			set;
		}
	}
}
