using System;
using System.Web.Mvc;
using MusicCompany.Common.Queries;
using MusicCompany.Website.Models.Comments;
using MusicCompany.Common.Commands;
using MusicCompany.Website.Models;

namespace MusicCompany.Website.Controllers
{
    public class CommentsController : ExtendedController
    {
		#region Public Properties

		public int PageSize
		{
			get;
			set;
		}

		#endregion

		/// <summary>
		/// Lists the comments for a top level work
		/// </summary>
		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Index(Guid id, int? page)
		{
			try
			{
				var query = new ListCommentsQuery();
				query.WorkId = id;
				query.Paging.Number = page ?? 1;
				query.Paging.Size = this.PageSize;
				var response = this.ProcessRequest<ListCommentsQueryResponse>(query);
				var model = new IndexViewModel();
				model.WorkId = id;
				model.Comments = response.Comments;
				return GetResult(View(model), View("CommentList", model));
			}
			catch ( Exception ex )
			{
				return GetFailure(ex, null);
			}
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Add(Guid workId)
		{
			try
			{
				if ( Request.IsAuthenticated )
				{
					var model = new CommentFormViewModel();
					model.Command = new CreateCommentCommand();
					model.Command.WorkId = workId;
					return GetResult(null, View("CommentForm", model));
				}
				else
				{
					var redirectResult = new RedirectResult(Url.Action("LogOn", "Account"));
					var model = new MusicCompany.Website.Models.Account.LogOnFormViewModel();
					var ajaxLogin = View("LogOnForm", model);
					return GetResult(redirectResult, ajaxLogin);
				}
			}
			catch ( Exception ex )
			{
				return this.GetFailure(ex, Index(workId, 1));
			}
		}

		[Authorize]
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Add(Guid workId, CreateCommentCommand command)
		{
			try
			{
				command.WorkId = workId;
				var response = this.ProcessRequest<CreateCommentCommandResponse>(command);
				UserMessage message = new UserMessage("Your Comment was added", UserMessageType.Info);
				return PostResult(message, Index(workId, 1));
			}
			catch ( Exception ex )
			{
				return this.PostFailure(ex, Index(workId, 1));
			}
		}
   }
}
