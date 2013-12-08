using Agatha.Common;
using MusicCompany.Common.Commands;
using MusicCompany.ServiceLayer.Base;

namespace MusicCompany.ServiceLayer.CommandHandlers
{
	public class DeleteWorkCommandHandler : CommandHandler<DeleteWorkCommand, DeleteWorkCommandResponse>
	{
		public override Response Handle(DeleteWorkCommand request)
		{
			var artist = this.GetArtistByIdentifier(request.ArtistIdentifier);
			this.CheckArtistManagementPermissions(request, artist);
			var work = artist.GetSingleWork(request.WorkIdentifier);
			artist.RemoveSingleWork(work);
			var response = this.CreateTypedResponse();
			response.WorkTitle = work.Title.Value;
			return response;
		}
	}
}