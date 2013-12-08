using System;
using NHibernate;
using Spring.Data.NHibernate.Support;
using Spring.Threading;

namespace MusicCompany.Infrastructure
{
	/// <summary>
	/// Defines the scope of one or more operations in the 
	/// system, so as to coordinate any infrastructural 
	/// resources (i.e  database connections) in a manner
	/// convenient and abstract to the client.
	/// </summary>
	/// <remarks>
	/// This essentially wraps a Spring.Data.NHibernate.SessionScope object,
	/// ensuring that is is managed properly in the current execution context.
	/// 
	/// A web application will need to create a new <see cref="UnitOfWork"/>
	/// for each request.  A Windows App or Windows Service can use a single
	/// <see cref="UnitOfWork"/> throughout it's lifetime or per operation as appropriate.  
	/// In either case, the database connections are opened and closed as needed without 
	/// the client needing to worry about it (too much).
	/// </remarks>
	/// <example>
	/// Use the <b>using</b> construct
	/// <code>
	/// using( UnitOfWork unitOfWork = new UnitOfWork() )
	/// {
	///    //do operations...
	/// }
	/// // unitOfWork is automatically disposed and database connections closed
	/// </code>
	/// Alternatively, a <b>try/finally</b> block can be used
	/// <code>
	/// UnitOfWork unitOfWork = new UnitOfWork();
	/// try
	/// {
	///     //do operations...
	/// }
	/// finally
	/// {
	///     unitOfWork.Dispose();
	/// }
	/// </code>
	/// </example>
	public class UnitOfWork : IDisposable
	{
		#region Private Fields

		private SessionScope sessionScope;

		#endregion

		#region Constructor(s)

		/// <summary>
		/// Static <see cref="UnitOfWork"/> initializer
		/// </summary>
		/// <remarks>
		/// At startup, set Spring's logical thread context storage to "hybrid",
		/// so that Sessions are scoped appropriately to either HttpContext or CallContext.
		/// This ensures that <see cref="UnitOfWork"/> instances operate corectly in all
		/// applications - (web, windows service, windows forms etc.)
		/// </remarks>
		static UnitOfWork()
		{
			LogicalThreadContext.SetStorage(new HybridContextStorage());
		}

		/// <summary>
		/// Initializes a new <see cref="UnitOfWork"/> instance
		/// </summary>
		/// <remarks>
		/// Here's where the SessionScope is created.
		/// </remarks>
		public UnitOfWork()
		{
			this.sessionScope = new SessionScope();
			this.sessionScope.SessionFactory.GetCurrentSession().FlushMode = FlushMode.Commit;
		}

		#endregion

		#region IDisposable Members

		/// <summary>
		/// Releases any resources being used
		/// </summary>
		/// <remarks>
		/// Disposes the SessionScope being held by this instance
		/// </remarks>
		public void Dispose()
		{
			this.sessionScope.Dispose();
		}

		#endregion
	}
}
