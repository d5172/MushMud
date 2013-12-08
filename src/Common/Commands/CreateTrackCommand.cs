using System.IO;

namespace MusicCompany.Common.Commands
{
	public class CreateTrackCommand : CommandRequestBase
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

	public class CreateTrackCommandResponse : CommandResponseBase
	{
		public string Title
		{
			get;
			set;
		}
	}
}