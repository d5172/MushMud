
namespace MusicCompany.Core.Services
{
	public interface IZipFileService
	{
		byte[] CreateZipFile(CollectionWork collectionWork);
	}
}