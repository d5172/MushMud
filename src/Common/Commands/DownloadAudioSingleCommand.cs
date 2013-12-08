
namespace MusicCompany.Common.Commands
{
	public class DownloadAudioSingleCommand : CommandRequestBase
	{
		public string ArtistIdentifier
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

	public class DownloadAudioSingleCommandResponse : FileResponse
	{
		
	}
}