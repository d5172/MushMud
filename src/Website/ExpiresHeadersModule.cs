using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using Elmah;
using System.Collections.Specialized;

namespace MusicCompany.Website
{
	/// <summary>
	/// This should add long expiration headers to certain files. 
	/// Reference: http://codebetter.com/blogs/karlseguin/archive/2010/01/08/asp-net-performance-part-2-yslow.aspx
	/// </summary>
	public class ExpiresHeadersModule : IHttpModule
	{
		private static readonly List<string> longCacheExtensions = new List<string> { ".js", ".css", ".png", ".jpg", ".gif", };

		public void Init(HttpApplication context)
		{
			context.EndRequest += new EventHandler(context_EndRequest);
		}

		private void context_EndRequest(object sender, EventArgs e)
		{
			var context = HttpContext.Current;
			var extension = Path.GetExtension(context.Request.Url.AbsolutePath);
			if ( longCacheExtensions.Contains(extension) )
			{
				context.Response.CacheControl = "private";
				context.Response.Expires = 44000; // at least 1 month
			}
		}

		public void Dispose()
		{
		}
	}
}
