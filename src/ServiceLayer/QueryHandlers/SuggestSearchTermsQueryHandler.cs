using System.Collections.Generic;
using System.Linq;
using Agatha.Common;
using MusicCompany.Common.Queries;
using MusicCompany.Common.ViewModel;
using MusicCompany.ServiceLayer.Base;
using NHibernate.Linq;

namespace MusicCompany.ServiceLayer.QueryHandlers
{
	public class SuggestSearchTermsQueryHandler : QueryHandler<SuggestSearchTermsQuery, SuggestSearchTermsQueryResponse>
	{
		public override Response Handle(SuggestSearchTermsQuery request)
		{
			List<string> matches = new List<string>();

			matches.AddRange(this.Session.Linq<ArtistSummaryView>().Where(a => a.Name.StartsWith(request.StartingWith)).Select(a => a.Name).ToList());
			matches.AddRange(this.Session.Linq<TopLevelWorkSummaryView>().Where(t => t.Title.StartsWith(request.StartingWith)).Select(t => t.Title).ToList());
			matches.AddRange(this.Session.Linq<AudioTrackSummaryView>().Where(t => t.Title.StartsWith(request.StartingWith)).Select(t => t.Title).ToList());
			matches.AddRange(this.Session.Linq<TagView>().Where(t => t.Lemma.StartsWith(request.StartingWith)).Select(t => t.Lemma).ToList());

			matches.Sort();

			var response = this.CreateTypedResponse();
			response.SuggestedTerms = matches.Distinct().ToArray();
			return response;
		}
	}
}