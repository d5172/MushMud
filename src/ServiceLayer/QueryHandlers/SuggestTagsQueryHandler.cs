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
	public class SuggestTagsQueryHandler : QueryHandler<SuggestTagsQuery, SuggestTagsQueryResponse>
	{
		public override Response Handle(SuggestTagsQuery request)
		{
			var response = new SuggestTagsQueryResponse();
			response.SuggestedTags = this.Session.Linq<TagView>()
				.Where(t => t.Lemma.StartsWith(request.StartingWith))
				.Select(t => t.Lemma)
				.ToArray();
			return response;
		}
	}
}