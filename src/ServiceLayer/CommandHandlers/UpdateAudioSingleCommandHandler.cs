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
	public class UpdateAudioSingleCommandHandler : CommandHandler<UpdateAudioSingleCommand, UpdateAudioSingleCommandResponse>
	{
		public ITagService TagService
		{
			get;
			set;
		}

		public override Response Handle(UpdateAudioSingleCommand request)
		{
			var artist = this.GetArtistByIdentifier(request.ArtistIdentifier);
			this.CheckArtistManagementPermissions(request, artist);
			var work = artist.GetSingleWork(request.WorkIdentifier);
			work.Description = request.Description;

			if ( request.Title != work.Title.Value )
			{
				artist.ChangeWorkTitle(work, request.Title);
			}
			if ( request.LicenseIdentifier != work.License.Abbreviation )
			{
				var license = this.Session.Linq<License>().Single(l => l.Abbreviation == request.LicenseIdentifier);
				work.ChangeLicense(license);
			}
			if ( work.ReleaseDate != request.ReleaseDate )
			{
				work.ChangeReleaseDate(request.ReleaseDate);
			}
			work.SetTags(request.Tags.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries), this.TagService);

			var response = this.CreateTypedResponse();
			response.Title = work.Title.Value;
			return response;

		}
	}
}