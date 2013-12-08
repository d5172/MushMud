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
	public class AddSingleToCollectionCommandHandler : CommandHandler<AddSingleToCollectionCommand, AddSingleToCollectionCommandResponse>
	{
		public override Response Handle(AddSingleToCollectionCommand request)
		{
			var artist = this.GetArtistByIdentifier(request.ArtistIdentifier);
			this.CheckArtistManagementPermissions(request, artist);
			var single = artist.GetSingleWork(request.WorkIdentifier);
			var collection = artist.GetCollectionWork(request.CollectionIdentifier);
			artist.AddWorkToParentWork(single, collection);
			var response = this.CreateTypedResponse();
			response.CollectionTitle = collection.Title.Value;
			response.WorkTitle = single.Title.Value;
			return response;
		}
	}
}