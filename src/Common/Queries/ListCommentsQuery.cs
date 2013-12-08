using System;
using System.Collections.Generic;
using MusicCompany.Common.ViewModel;
using PagedList;

namespace MusicCompany.Common.Queries
{
	public class ListCommentsQuery : QueryRequestBase
	{
		public Guid WorkId
		{
			get;
			set;
		}
	}

	public class ListCommentsQueryResponse : QueryResponseBase
	{
		public IPagedList<CommentView> Comments
		{
			get;
			set;
		}
	}
}
