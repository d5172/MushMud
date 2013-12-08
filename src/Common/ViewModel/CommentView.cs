using System;

namespace MusicCompany.Common.ViewModel
{
	public class CommentView
	{
		public Guid Id
		{
			get;
			set;
		}

		public Guid WorkId
		{
			get;
			set;
		}

		public string Username
		{
			get;
			set;
		}

		public string CommentText
		{
			get;
			set;
		}

		public DateTime DateEntered
		{
			get;
			set;
		}
	}
}