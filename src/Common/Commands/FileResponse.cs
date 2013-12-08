
namespace MusicCompany.Common.Commands
{
	public class FileResponse : CommandResponseBase
	{
		public string FileName
		{
			get;
			set;
		}

		public string MimeType
		{
			get;
			set;
		}

		public byte[] Data
		{
			get;
			set;
		}
	}
}
