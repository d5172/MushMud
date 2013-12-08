using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MusicCompany.Common.ViewModel;
using PagedList;

namespace MusicCompany.Common.Queries
{
	public class ListTopLevelWorksQuery : QueryRequestBase
	{

	}

	public class ListTopLevelWorksResponse : QueryResponseBase
	{
		public IPagedList<TopLevelWorkSummaryView> Works
		{
			get;
			set;
		}
	}
}
