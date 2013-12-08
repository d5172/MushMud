using System;
using System.Linq;
using Agatha.Common;
using MusicCompany.Common.Commands;
using MusicCompany.Core;
using MusicCompany.ServiceLayer.Base;
using NHibernate;
using NHibernate.Linq;

namespace MusicCompany.ServiceLayer.CommandHandlers
{
	public class DeleteArtistCommandHandler : CommandHandler<DeleteArtistCommand, DeleteArtistCommandResponse>
	{
		public override Response Handle(DeleteArtistCommand request)
		{
			Artist artist = this.GetArtistByIdentifier(request.Identifier);
			this.CheckArtistManagementPermissions(request, artist);
			this.Session.Delete(artist);
			var response =  this.CreateTypedResponse();
			response.ArtistName = artist.Name.Value;
			return response;
		}
	}
}