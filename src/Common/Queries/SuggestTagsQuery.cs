using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagedList;
using MusicCompany.Common.ViewModel;

namespace MusicCompany.Common.Queries
{
	public class SuggestTagsQuery : QueryRequestBase
	{
		public string StartingWith
		{
			get;
			set;
		}
	}

	public class SuggestTagsQueryResponse : QueryResponseBase
	{
		public string[] SuggestedTags
		{
			get;
			set;
		}
	}
}
