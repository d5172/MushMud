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
	public class ChangeArtistNameCommandHandler : CommandHandler<ChangeArtistNameCommand, ChangeArtistNameCommandResponse>
	{
		public override Response Handle(ChangeArtistNameCommand request)
		{
			Artist artist = this.GetArtistByIdentifier(request.ArtistIdentifier);
			this.CheckArtistManagementPermissions(request, artist);
			artist.ChangeName(request.NewName);
			var response =  this.CreateTypedResponse();
			response.ArtistName = artist.Name.Value;
			return response;
		}
	}
}