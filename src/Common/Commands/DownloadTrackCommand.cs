
namespace MusicCompany.Common.Commands
{
	public class DownloadTrackCommand : CommandRequestBase
	{
		public string ArtistIdentifier
		{
			get;
			set;
		}

		public string CollectionIdentifier
		{
			get;
			set;
		}

		public string WorkIdentifier
		{
			get;
			set;
		}

		public string FileFormat
		{
			get;
			set;
		}
	}

	public class DownloadTrackCommandResponse : FileResponse
	{
		
	}
}