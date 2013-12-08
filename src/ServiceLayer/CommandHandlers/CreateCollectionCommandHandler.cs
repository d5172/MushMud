using System;
using System.Linq;
using Agatha.Common;
using MusicCompany.Common.Commands;
using MusicCompany.Core;
using MusicCompany.ServiceLayer.Base;
using NHibernate;
using NHibernate.Linq;
using MusicCompany.Core.Services;

namespace MusicCompany.ServiceLayer.CommandHandlers
{
	public class CreateCollectionCommandHandler : CommandHandler<CreateCollectionCommand, CreateCollectionCommandResponse>
	{
		public ITagService TagService
		{
			get;
			set;
		}

		public override Response Handle(CreateCollectionCommand request)
		{
			var artist = this.GetArtistByIdentifier(request.ArtistIdentifier);
			this.CheckArtistManagementPermissions(request, artist);
			var license = this.Session.Linq<License>().SingleOrDefault(l => l.Abbreviation == request.LicenseIdentifier);
			CollectionWork collection = new CollectionWork(artist, request.Title, license, request.ReleaseDate);
			collection.Description = request.Description;
			collection.SetTags(request.Tags.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries), this.TagService);
			this.Session.Save(collection);
			var response = this.CreateTypedResponse();
			response.CollectionTitle = collection.Title.Value;
			return response;
		}
	}
}