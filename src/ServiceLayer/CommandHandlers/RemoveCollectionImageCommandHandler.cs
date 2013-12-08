using Agatha.Common;
using MusicCompany.Common.Commands;
using MusicCompany.ServiceLayer.Base;

namespace MusicCompany.ServiceLayer.CommandHandlers
{
	public class RemoveCollectionImageCommandHandler : CommandHandler<RemoveCollectionImageCommand, RemoveCollectionImageCommandResponse>
	{
		public override Response Handle(RemoveCollectionImageCommand request)
		{
			var artist = this.GetArtistByIdentifier(request.ArtistIdentifier);
			this.CheckArtistManagementPermissions(request, artist);
			var collection = artist.GetCollectionWork(request.CollectionIdentifier);
			collection.RemoveImageFile();
			return this.CreateTypedResponse();
		}
	}
}