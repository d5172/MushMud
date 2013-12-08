
namespace MusicCompany.Common.Commands
{
	public class RemoveCollectionImageCommand : CommandRequestBase
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

	public class RemoveCollectionImageCommandResponse : CommandResponseBase
	{

	}
}