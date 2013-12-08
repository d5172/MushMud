
namespace MusicCompany.Common.Commands
{
	public class DownloadCollectionZipCommand : CommandRequestBase
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

	public class DownloadCollectionZipCommandResponse : FileResponse
	{

	}
}