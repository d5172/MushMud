using System.IO;
using Agatha.Common;
using MusicCompany.Common.Commands;
using MusicCompany.Core;
using MusicCompany.Core.Factories;
using MusicCompany.ServiceLayer.Base;

namespace MusicCompany.ServiceLayer.CommandHandlers
{
	public class CreateTrackCommandHandler : CommandHandler<CreateTrackCommand, CreateTrackCommandResponse>
	{
		public IAudioFileInfoFactory AudioFileInfoFactory
		{
			get;
			set;
		}

		public override Response Handle(CreateTrackCommand request)
		{
			Artist artist = this.GetArtistByIdentifier(request.ArtistIdentifier);
			this.CheckArtistManagementPermissions(request, artist);
			CollectionWork collection = artist.GetCollectionWork(request.CollectionIdentifier);

			string extension = Path.GetExtension(request.FileName);
			FileFormat inputFormat = extension.ToFileFormat();

			AudioFileInfo audioFileInfo = this.AudioFileInfoFactory.CreateAudio(request.FileName, inputFormat, request.InputStream);

			AudioWork audioWork = new AudioWork(artist, Path.GetFileNameWithoutExtension(request.FileName), audioFileInfo, collection.License, collection.ReleaseDate);
			artist.AddWorkToParentWork(audioWork, collection);

			var response = this.CreateTypedResponse();
			response.Title = audioWork.Title.Value;
			return response;
		}
	}
}