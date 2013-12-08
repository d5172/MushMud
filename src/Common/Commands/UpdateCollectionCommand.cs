
using System;
namespace MusicCompany.Common.Commands
{
	public class UpdateCollectionCommand : CollectionCommandBase
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
	}

	public class UpdateCollectionCommandResponse : CommandResponseBase
	{
		public string Title
		{
			get;
			set;
		}
	}
}