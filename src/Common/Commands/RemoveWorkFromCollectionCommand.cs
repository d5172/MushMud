
namespace MusicCompany.Common.Commands
{
	public class RemoveWorkFromCollectionCommand : CommandRequestBase
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
		public string WorkIdentifier
		{
			get;
			set;
		}
	}

	public class RemoveWorkFromCollectionCommandResponse : CommandResponseBase
	{
		public string WorkTitle
		{
			get;
			set;
		}

		public string CollectionTitle
		{
			get;
			set;
		}
	}
}