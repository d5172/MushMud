using MusicCompany.Common.ViewModel;

namespace MusicCompany.Common.Queries
{
	public class GetPersonDetailQuery : QueryRequestBase
	{
		public string Username
		{
			get;
			set;
		}
	}

	public class GetPersonDetailQueryResponse : QueryResponseBase
	{
		public PersonDetailView Person
		{
			get;
			set;
		}
	}
}
