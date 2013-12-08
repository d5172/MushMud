using Agatha.Common;
using MusicCompany.Common.Commands;
using MusicCompany.ServiceLayer.Base;

namespace MusicCompany.ServiceLayer.CommandHandlers
{
	public class DeleteCollectionCommandHandler : CommandHandler<DeleteCollectionCommand, DeleteCollectionCommandResponse>
	{
		public override Response Handle(DeleteCollectionCommand request)
		{
			var artist = this.GetArtistByIdentifier(request.ArtistIdentifier);
			this.CheckArtistManagementPermissions(request, artist);
			var collection = artist.GetCollectionWork(request.CollectionIdentifier);
			artist.RemoveCollectionWork(collection);
			var response = this.CreateTypedResponse();
			response.CollectionTitle = collection.Title.Value;
			return response;
		}	
	}
}