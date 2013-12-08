using System.Collections.Generic;
using System.Linq;
using Agatha.Common;
using MusicCompany.Common.Queries;
using MusicCompany.Common.ViewModel;
using MusicCompany.ServiceLayer.Base;
using NHibernate.Linq;
using PagedList;
using NHibernate;
using NHibernate.Criterion;

namespace MusicCompany.ServiceLayer.QueryHandlers
{
	public class WorkSearchQueryHandler : QueryHandler<WorkSearchQuery, WorkSearchQueryResponse>
	{
		public override Response Handle(WorkSearchQuery request)
		{
			DetachedCriteria inTracks = DetachedCriteria.For<AudioTrackSummaryView>();
			Disjunction orTracks = Expression.Disjunction();
			foreach ( string searchTerm in request.SearchTerms )
			{
				orTracks.Add(Expression.Like("Title", searchTerm, MatchMode.Anywhere));
				orTracks.Add(Expression.Like("Tags", searchTerm, MatchMode.Anywhere));
			}
			inTracks.Add(orTracks);
			inTracks.SetProjection(Projections.Property("CollectionIdentifier"));

			ICriteria criteria = this.Session.CreateCriteria(typeof(TopLevelWorkSummaryView));
			Disjunction or = Expression.Disjunction();
			foreach ( string searchTerm in request.SearchTerms )
			{
				or.Add(Expression.Like("Title", searchTerm, MatchMode.Anywhere));
				or.Add(Expression.Like("ArtistName", searchTerm, MatchMode.Anywhere));
				or.Add(Expression.Like("Tags", searchTerm, MatchMode.Anywhere));
			}
			or.Add(Subqueries.PropertyIn("Identifier", inTracks));
			
			criteria.Add(or);
			criteria.Add(Expression.Eq("IsReleased", true));
			criteria.AddOrder(Order.Asc("Title"));
			//criteria.SetMaxResults(request.Paging.Size);
			//criteria.SetFirstResult(request.Paging.GetSkipIndex());
			IList<TopLevelWorkSummaryView> result = criteria.List<TopLevelWorkSummaryView>();

			var response = this.CreateTypedResponse();
			//response.Works = new StaticPagedList<TopLevelWorkSummaryView>(result, request.Paging.ItemIndex(), request.Paging.Size, int.MaxValue);
			response.Works = new PagedList<TopLevelWorkSummaryView>(result, request.Paging.ItemIndex(), request.Paging.Size);
			return response;
		}
	}
}