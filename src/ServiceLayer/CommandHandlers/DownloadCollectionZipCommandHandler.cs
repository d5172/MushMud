using Agatha.Common;
using MusicCompany.Common.Commands;
using MusicCompany.Core;
using MusicCompany.Core.Services;
using MusicCompany.ServiceLayer.Base;

namespace MusicCompany.ServiceLayer.CommandHandlers
{
	public class DownloadCollectionZipCommandHandler : CommandHandler<DownloadCollectionZipCommand, DownloadCollectionZipCommandResponse>
	{
		public IZipFileService ZipFileService
		{
			get;
			set;
		}

		public override Response Handle(DownloadCollectionZipCommand request)
		{
			var artist = this.GetArtistByIdentifier(request.ArtistIdentifier);
			var collection = artist.GetCollectionWork(request.CollectionIdentifier);

			var person = this.GetPersonFromCommandContext(request);
			collection.LogDownloadEvent(person);

			var response = this.CreateTypedResponse();
			response.FileName = collection.ToFileName(".zip");
			response.MimeType = FileFormat.Zip.ToMimeType();
			response.Data = this.ZipFileService.CreateZipFile(collection);
			return response;
		}
	}
}