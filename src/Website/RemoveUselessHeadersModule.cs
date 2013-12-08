using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web;
using Elmah;

namespace MusicCompany.Website
{
	/// <summary>
	/// This should Remove useless response headers
	/// 
	/// Note: This only works in II7 integrated pipeline mode.
	/// 
	/// Reference: http://codebetter.com/blogs/karlseguin/archive/2010/01/08/asp-net-performance-part-2-yslow.aspx
	/// </summary>
	public class RemoveUselessHeadersModule : IHttpModule
	{
		private static readonly List<string> headersToRemove = new List<string> { "X-AspNet-Version", "X-AspNetMvc-Version", "Etag", "Server", };

		#region IHttpModule Members

		public void Dispose()
		{
		}

		public void Init(HttpApplication context)
		{
			context.PreSendRequestHeaders += new EventHandler(context_PreSendRequestHeaders);
		}

		void context_PreSendRequestHeaders(object sender, EventArgs e)
		{
			try
			{
				HttpApplication app = sender as HttpApplication;
				if ( null != app && null != app.Request && !app.Request.IsLocal && null != app.Context && null != app.Context.Response )
				{
					NameValueCollection headers = app.Context.Response.Headers;
					if ( null != headers )
					{
						headersToRemove.ForEach(h => headers.Remove(h));
					}
				}
			}
			catch ( Exception ex )
			{
				try
				{
					ErrorSignal.FromCurrentContext().Raise(ex);
				}
				catch
				{
					//oh well....
				}
			}
		}

		#endregion
	}
}
