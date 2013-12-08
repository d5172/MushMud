using Agatha.Common;

namespace MusicCompany.Common.Queries
{
	public abstract class QueryRequestBase : Request
	{
		public QueryRequestBase()
		{
			this.Paging = new PagingSpecification();
		}

		public PagingSpecification Paging
		{
			get;
			set;
		}
	}

	public abstract class QueryResponseBase : Response
	{

	}
}
