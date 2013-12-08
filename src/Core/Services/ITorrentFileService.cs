
namespace MusicCompany.Core.Services
{
	public interface ITorrentFileService
	{
		byte[] CreateTorrentFile(CollectionWork collectionWork);
	}
}
