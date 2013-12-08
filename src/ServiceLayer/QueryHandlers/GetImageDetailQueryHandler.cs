using Agatha.Common;
using MusicCompany.Common.Queries;
using MusicCompany.Common.ViewModel;
using MusicCompany.ServiceLayer.Base;

namespace MusicCompany.ServiceLayer.QueryHandlers
{
	public class GetImageDetailQueryHandler : QueryHandler<GetImageDetailQuery, GetImageDetailResponse>
	{
		public override Response Handle(GetImageDetailQuery request)
		{
			var response = this.CreateTypedResponse();
			response.Image = this.Session.Get<ImageDetailView>(request.Id);
			return response;
		}
	}
}