using MusicCompany.Common.ViewModel;
using System;

namespace MusicCompany.Common.Queries
{
	public class GetImageDetailQuery : QueryRequestBase
	{
		public Guid Id
		{
			get;
			set;
		}
	}

	public class GetImageDetailResponse : QueryResponseBase
	{
		public ImageDetailView Image
		{
			get;
			set;
		}
	}
}
