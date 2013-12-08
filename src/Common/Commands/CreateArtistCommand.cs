
using System;
namespace MusicCompany.Common.Commands
{
	public class CreateArtistCommand : CommandRequestBase
	{
		public string OwningPersonUsername
		{
			get;
			set;
		}

		public string ArtistName
		{
			get;
			set;
		}

		public string Bio
		{
			get;
			set;
		}
	}

	public class CreateArtistCommandResponse : CommandResponseBase
	{
		public string ArtistName
		{
			get;
			set;
		}
	}
}