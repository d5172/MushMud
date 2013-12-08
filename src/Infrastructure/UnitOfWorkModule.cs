using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace MusicCompany.Infrastructure
{
	public class UnitOfWorkModule : IHttpModule
	{
		#region Private Fields

		private UnitOfWork unitOfWork;

		#endregion

		#region IHttpModule Members

		public void Dispose()
		{
		}

		public void Init(HttpApplication context)
		{
			context.BeginRequest += new EventHandler(context_BeginRequest);
			context.EndRequest += new EventHandler(context_EndRequest);
			context.Error += new EventHandler(context_Error);
		}

		private void context_BeginRequest(object sender, EventArgs e)
		{
			this.unitOfWork = new UnitOfWork();
		}

		private void context_EndRequest(object sender, EventArgs e)
		{
			this.unitOfWork.Dispose();
		}

		private void context_Error(object sender, EventArgs e)
		{
			this.unitOfWork.Dispose();
		}

		#endregion
	}
}
