using System;
using System.IO;

namespace MusicCompany.Common.Commands
{
	public class UpdateArtistProfilePictureCommand : CommandRequestBase
	{
		public string ArtistIdentifier
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

	public class UpdateArtistProfilePictureCommandResponse : CommandResponseBase
	{
		public Guid NewImageId
		{
			get;
			set;
		}
	}
}