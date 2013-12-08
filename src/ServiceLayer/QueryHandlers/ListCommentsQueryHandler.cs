using System.Collections.Generic;
using System.Linq;
using Agatha.Common;
using MusicCompany.Common.Queries;
using MusicCompany.Common.ViewModel;
using MusicCompany.ServiceLayer.Base;
using NHibernate.Linq;
using PagedList;

namespace MusicCompany.ServiceLayer.QueryHandlers
{
	public class ListCommentsQueryHandler : QueryHandler<ListCommentsQuery, ListCommentsQueryResponse>
	{
		public override Response Handle(ListCommentsQuery request)
		{
			int count = this.Session.Linq<CommentView>().Count(c => c.WorkId == request.WorkId);
			
			IList<CommentView> result;
			if ( count > 0 )
			{
				result = this.Session.Linq<CommentView>()
					.Where(c => c.WorkId == request.WorkId)
					.OrderByDescending(d => d.DateEntered)
					.Skip(request.Paging.GetSkipIndex())
					.Take(request.Paging.Size)
					.ToList();
			}
			else
			{
				result = new List<CommentView>();
			}

			var response = this.CreateTypedResponse();
			response.Comments = new StaticPagedList<CommentView>(result, request.Paging.ItemIndex(), request.Paging.Size, count);

			return response;
		}
	}
}