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
	public class UpdateCollectionCommandHandler : CommandHandler<UpdateCollectionCommand, UpdateCollectionCommandResponse>
	{
		public ITagService TagService
		{
			get;
			set;
		}

		public override Response Handle(UpdateCollectionCommand request)
		{
			var artist = this.GetArtistByIdentifier(request.ArtistIdentifier);
			this.CheckArtistManagementPermissions(request, artist);
			var collection = artist.GetCollectionWork(request.CollectionIdentifier);
			if ( collection.ReleaseDate != request.ReleaseDate )
			{
				collection.ChangeReleaseDate(request.ReleaseDate);
			}
			var license = this.Session.Linq<License>().Single(l => l.Abbreviation == request.LicenseIdentifier);
			if ( license != collection.License )
			{
				collection.ChangeLicense(license);
			}
			collection.Description = request.Description;
			collection.SetTags(request.Tags.Split(new char[]{','}, StringSplitOptions.RemoveEmptyEntries), this.TagService);
			if ( collection.Title.Value != request.Title )
			{
				artist.ChangeCollectionTitle(collection, request.Title);
			}
			var response = this.CreateTypedResponse();
			response.Title = collection.Title.Value;
			return response;
		}
	}
}