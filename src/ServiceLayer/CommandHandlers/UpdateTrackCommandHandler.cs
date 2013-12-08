using System;
using Agatha.Common;
using MusicCompany.Common.Commands;
using MusicCompany.Core;
using MusicCompany.Core.Services;
using MusicCompany.ServiceLayer.Base;

namespace MusicCompany.ServiceLayer.CommandHandlers
{
	public class UpdateTrackCommandHandler : CommandHandler<UpdateTrackCommand, UpdateTrackCommandResponse>
	{
		public ITagService TagService
		{
			get;
			set;
		}

		public override Response Handle(UpdateTrackCommand request)
		{
			var artist = this.GetArtistByIdentifier(request.ArtistIdentifier);
			this.CheckArtistManagementPermissions(request, artist);
			var collection = artist.GetCollectionWork(request.CollectionIdentifier);
			var track = collection.GetWork(request.WorkIdentifier) as AudioWork;
			if ( request.Title != track.Title.Value )
			{
				artist.ChangeWorkTitle(track, request.Title);
			}
			track.Description = request.Description;
			track.SetTags(request.Tags.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries), this.TagService);

			var response = this.CreateTypedResponse();
			response.Title = track.Title.Value;
			return response;
		}
	}
}