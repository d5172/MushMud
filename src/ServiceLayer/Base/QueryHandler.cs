using MusicCompany.Common.Queries;

namespace MusicCompany.ServiceLayer.Base
{
	public abstract class QueryHandler<TRequest, TResponse> : TransactionalRequestHandler<TRequest, TResponse>
		where TRequest : QueryRequestBase
		where TResponse : QueryResponseBase
	{
	}
}
