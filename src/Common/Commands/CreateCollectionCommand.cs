using System;

namespace MusicCompany.Common.Commands
{
	public class CreateCollectionCommand : CollectionCommandBase
	{
		public string ArtistIdentifier
		{
			get;
			set;
		}
	}

	public class CreateCollectionCommandResponse : CommandResponseBase
	{
		public string CollectionTitle
		{
			get;
			set;
		}
	}
}