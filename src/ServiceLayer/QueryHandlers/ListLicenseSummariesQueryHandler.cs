using System.Linq;
using Agatha.Common;
using MusicCompany.Common.Queries;
using MusicCompany.Common.ViewModel;
using MusicCompany.ServiceLayer.Base;
using NHibernate.Linq;

namespace MusicCompany.ServiceLayer.QueryHandlers
{
	public class ListLicenseSummariesQueryHandler : QueryHandler<ListLicenseSummariesQuery, ListLicenseSummariesQueryResponse>
	{
		public override Response Handle(ListLicenseSummariesQuery request)
		{
			var response = this.CreateTypedResponse();
			response.LicenseSummaries = this.Session.Linq<LicenseSummaryView>()
				.OrderBy(l => l.ViewOrder).ToList();
			return response;
		}
	}
}