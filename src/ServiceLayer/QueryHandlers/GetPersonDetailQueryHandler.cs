using System.Linq;
using Agatha.Common;
using MusicCompany.Common.Queries;
using MusicCompany.Common.ViewModel;
using MusicCompany.ServiceLayer.Base;
using NHibernate.Linq;

namespace MusicCompany.ServiceLayer.QueryHandlers
{
	public class GetPersonDetailQueryHandler : QueryHandler<GetPersonDetailQuery, GetPersonDetailQueryResponse>
	{
		public override Response Handle(GetPersonDetailQuery request)
		{
			var response = this.CreateTypedResponse();
			response.Person = this.Session.Linq<PersonDetailView>()
				.Single(p => p.Username == request.Username);
			return response;
		}
	}
}