using Agatha.Common;
using Agatha.ServiceLayer;
using NHibernate;

namespace MusicCompany.ServiceLayer.Base
{
	public abstract class TransactionalRequestHandler<TRequest, TResponse> : RequestHandler<TRequest, TResponse>
		where TRequest : Request
		where TResponse : Response
	{

		public ISessionFactory SessionFactory
		{
			get;
			set;
		}

		protected ISession Session
		{
			get
			{
				return this.SessionFactory.GetCurrentSession();
			}
		}

		public override Response Handle(Request request)
		{
			using ( ITransaction transaction = this.Session.BeginTransaction() )
			{
				Response response;
				try
				{
					response = base.Handle(request);
					if ( transaction.IsActive )
					{
						transaction.Commit();
					}
				}
				catch
				{
					if ( transaction != null && transaction.IsActive )
					{
						transaction.Rollback();
					}
					throw;
				}
				return response;
			}
		}
	}
}
