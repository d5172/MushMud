
namespace MusicCompany.Common.Commands
{
	public class DownloadCollectionTorrentCommand : CommandRequestBase
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

	public class DownloadCollectionTorrentCommandResponse : FileResponse
	{

	}
}