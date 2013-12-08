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
	public class UpdateArtistProfileCommandHandler : CommandHandler<UpdateArtistProfileCommand, UpdateArtistProfileCommandResponse>
	{
		public override Response Handle(UpdateArtistProfileCommand request)
		{
			var artist = this.GetArtistByIdentifier(request.Identifier);
			artist.Bio = request.Bio;
			return this.CreateTypedResponse();
		}
	}
}