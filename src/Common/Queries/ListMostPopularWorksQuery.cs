using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MusicCompany.Common.ViewModel;
using PagedList;

namespace MusicCompany.Common.Queries
{
	public class ListMostPopularWorksQuery : QueryRequestBase
	{

	}

	public class ListMostPopularWorksResponse : QueryResponseBase
	{
		public IPagedList<TopLevelWorkSummaryView> Works
		{
			get;
			set;
		}
	}
}
