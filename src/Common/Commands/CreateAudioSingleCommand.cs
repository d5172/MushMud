using System.IO;
namespace MusicCompany.Common.Commands
{
	public class CreateAudioSingleCommand : CommandRequestBase
	{
		public string ArtistIdentifier
		{
			get;
			set;
		}

		public string License
		{
			get;
			set;
		}

		public Stream InputStream
		{
			get;
			set;
		}

		public string FileName
		{
			get;
			set;
		}
	}

	public class CreateAudioSingleCommandResponse : CommandResponseBase
	{
		public string Title
		{
			get;
			set;
		}
	}
}