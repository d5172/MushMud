
namespace MusicCompany.Common.Commands
{
	public class AddSingleToCollectionCommand : CommandRequestBase
	{
		public string ArtistIdentifier
		{
			get;
			set;
		}

		public string WorkIdentifier
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

	public class AddSingleToCollectionCommandResponse : CommandResponseBase
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