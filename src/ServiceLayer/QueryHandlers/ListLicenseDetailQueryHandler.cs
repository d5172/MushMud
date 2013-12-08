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
	public class ListLicenseDetailQueryHandler : QueryHandler<ListLicenseDetailQuery, ListLicenseDetailQueryResponse>
	{
		public override Response Handle(ListLicenseDetailQuery request)
		{
			var response = this.CreateTypedResponse();
			response.Licenses = this.Session.Linq<LicenseDetailView>()
				.OrderBy(l => l.ViewOrder).ToList();
			return response;
		}
	}
}