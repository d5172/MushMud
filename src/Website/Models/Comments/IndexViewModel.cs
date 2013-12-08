using System;
using MusicCompany.Common.ViewModel;
using PagedList;
using MusicCompany.Common.Commands;

namespace MusicCompany.Website.Models.Comments
{
	public class IndexViewModel
	{
		public Guid WorkId
		{
			get;
			set;
		}

		public IPagedList<CommentView> Comments
		{
			get;
			set;
		}
	}
}