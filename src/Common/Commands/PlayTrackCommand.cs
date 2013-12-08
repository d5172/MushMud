
namespace MusicCompany.Common.Commands
{
	public class PlayTrackCommand : CommandRequestBase
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

		public bool SkipEventLogging
		{
			get;
			set;
		}
	}

	public class PlayTrackCommandResponse : FileResponse
	{
		
	}
}