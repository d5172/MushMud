using System.Linq;
using Agatha.Common;
using MusicCompany.Common.Commands;
using MusicCompany.Core;
using MusicCompany.ServiceLayer.Base;
using NHibernate.Linq;

namespace MusicCompany.ServiceLayer.CommandHandlers
{
	public class CreateArtistCommandHandler :CommandHandler<CreateArtistCommand, CreateArtistCommandResponse>
	{
		public override Response Handle(CreateArtistCommand request)
		{
			Person owner = this.GetPersonByIdentifier(request.OwningPersonUsername);
			Artist artist = new Artist(request.ArtistName, owner);
			artist.Bio = request.Bio ?? "";
			this.Session.Save(artist);
			var response = this.CreateTypedResponse();
			response.ArtistName = artist.Name.Value;
			return response;
		}
	}
}