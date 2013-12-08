using System.Web;

namespace MusicCompany.Infrastructure
{
	public interface IFilePath
	{
		string GetPath();
	}

	public class WebServerFilePath : IFilePath
	{
		public string VirtualPath
		{
			get;
			set;
		}

		#region IFilePath Members

		public string GetPath()
		{
			return HttpContext.Current.Server.MapPath(this.VirtualPath);
		}

		#endregion
	}
}
