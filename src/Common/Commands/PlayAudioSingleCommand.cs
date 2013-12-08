
namespace MusicCompany.Common.Commands
{
	public class PlayAudioSingleCommand : CommandRequestBase
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

		public bool SkipEventLogging
		{
			get;
			set;
		}
	}

	public class PlayAudioSingleCommandResponse : FileResponse
	{
		
	}
}