using Agatha.Common;
using MusicCompany.Common.Commands;
using MusicCompany.Core;
using MusicCompany.ServiceLayer.Base;

namespace MusicCompany.ServiceLayer.CommandHandlers
{
	public class CreateCommentHandler : CommandHandler<CreateCommentCommand, CreateCommentCommandResponse>
	{
		public override Response Handle(CreateCommentCommand request)
		{
			var person = this.GetPersonFromCommandContext(request);
			var work = this.Session.Load<Work>(request.WorkId);
			var comment = new Comment(work, person, request.CommentText);
			work.LogCommentEvent(person);
			this.Session.Save(comment);
			var response = this.CreateTypedResponse();
			return response;
		}
	}
}