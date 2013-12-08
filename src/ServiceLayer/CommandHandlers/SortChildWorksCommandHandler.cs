using System;
using System.Collections.Generic;
using System.Linq;
using Agatha.Common;
using MusicCompany.Common.Commands;
using MusicCompany.Core;
using MusicCompany.ServiceLayer.Base;

namespace MusicCompany.ServiceLayer.CommandHandlers
{
	public class SortChildWorksCommandHandler : CommandHandler<SortChildWorksCommand, SortChildWorksCommandResponse>
	{
		public override Response Handle(SortChildWorksCommand request)
		{
			var artist = this.GetArtistByIdentifier(request.ArtistIdentifier);
			this.CheckArtistManagementPermissions(request, artist);
			var collection = artist.GetCollectionWork(request.CollectionIdentifier);
			IList<Work> sortedWorks = new List<Work>();
			foreach (string identifier in request.SortedIdentifiers)
			{
				sortedWorks.Add(collection.GetWork(identifier));
			}
			collection.SortWorks(sortedWorks);
			var response = this.CreateTypedResponse();
			response.CollectionTitle = collection.Title.Value;
			return response;
		}
	}
}