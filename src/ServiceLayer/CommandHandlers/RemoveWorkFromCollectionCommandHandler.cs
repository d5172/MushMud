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
	public class RemoveWorkFromCollectionCommandHandler : CommandHandler<RemoveWorkFromCollectionCommand, RemoveWorkFromCollectionCommandResponse>
	{
		public override Response Handle(RemoveWorkFromCollectionCommand request)
		{
			var artist = this.GetArtistByIdentifier(request.ArtistIdentifier);
			this.CheckArtistManagementPermissions(request, artist);
			var collection = artist.GetCollectionWork(request.CollectionIdentifier);
			var work = collection.GetWork(request.WorkIdentifier);
			artist.RemoveWorkFromParent(work);
			var response = this.CreateTypedResponse();
			response.WorkTitle = work.Title.Value;
			response.CollectionTitle = collection.Title.Value;
			return response;
		}
	}
}