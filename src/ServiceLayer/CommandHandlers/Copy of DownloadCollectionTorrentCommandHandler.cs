using Agatha.Common;
using MusicCompany.Common.Commands;
using MusicCompany.Core;
using MusicCompany.Core.Services;
using MusicCompany.ServiceLayer.Base;

namespace MusicCompany.ServiceLayer.CommandHandlers
{
	public class DownloadCollectionTorrentCommandHandler : CommandHandler<DownloadCollectionTorrentCommand, DownloadCollectionTorrentCommandResponse>
	{
		public ITorrentFileService TorrentFileService
		{
			get;
			set;
		}

		public override Response Handle(DownloadCollectionTorrentCommand request)
		{
			var artist = this.GetArtistByIdentifier(request.ArtistIdentifier);
			var collection = artist.GetCollectionWork(request.CollectionIdentifier);

			var person = this.GetPersonFromCommandContext(request);
			collection.LogDownloadEvent(person);

			var response = this.CreateTypedResponse();
			response.FileName = collection.ToFileName(".torrent");
			response.MimeType = FileFormat.Torrent.ToMimeType();
			response.Data = this.TorrentFileService.CreateTorrentFile(collection);
			return response;
		}
	}
}