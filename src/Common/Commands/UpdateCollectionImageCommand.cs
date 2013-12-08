using System.IO;
using System;

namespace MusicCompany.Common.Commands
{
	public class UpdateCollectionImageCommand : CommandRequestBase
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

		public string FileName
		{
			get;
			set;
		}

		public Stream InputStream
		{
			get;
			set;
		}

	}

	public class UpdateCollectionImageCommandResponse : CommandResponseBase
	{
		public Guid NewImageId
		{
			get;
			set;
		}

	}
}