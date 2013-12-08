using System;

namespace MusicCompany.Common.Commands
{
	public class ChangeArtistNameCommand : CommandRequestBase
	{
		public string ArtistIdentifier
		{
			get;
			set;
		}

		public string NewName
		{
			get;
			set;
		}

	}

	public class ChangeArtistNameCommandResponse : CommandResponseBase
	{
		public string ArtistName
		{
			get;
			set;
		}
	}
}