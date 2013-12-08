
namespace MusicCompany.Common.Commands
{
	public class DeleteCollectionCommand : CommandRequestBase
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

	public class DeleteCollectionCommandResponse : CommandResponseBase
	{
		public string CollectionTitle
		{
			get;
			set;
		}
	}
}