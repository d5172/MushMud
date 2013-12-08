using System;

namespace MusicCompany.Common.Commands
{
	public class CreateCommentCommand : CommandRequestBase
	{
		public Guid WorkId
		{
			get;
			set;
		}

		public string CommentText
		{
			get;
			set;
		}
	}

	public class CreateCommentCommandResponse : CommandResponseBase
	{
	}
}