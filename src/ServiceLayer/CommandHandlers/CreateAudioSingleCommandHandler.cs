using System;
using System.IO;
using System.Linq;
using Agatha.Common;
using MusicCompany.Common.Commands;
using MusicCompany.Core;
using MusicCompany.ServiceLayer.Base;
using NHibernate.Linq;
using MusicCompany.Core.Factories;

namespace MusicCompany.ServiceLayer.CommandHandlers
{
	public class CreateAudioSingleCommandHandler : CommandHandler<CreateAudioSingleCommand, CreateAudioSingleCommandResponse>
	{
		public IAudioFileInfoFactory AudioFileInfoFactory
		{
			get;
			set;
		}

		public override Response Handle(CreateAudioSingleCommand request)
		{
			Artist artist = this.GetArtistByIdentifier(request.ArtistIdentifier);
			this.CheckArtistManagementPermissions(request, artist);

			string extension = Path.GetExtension(request.FileName);
			FileFormat inputFormat = extension.ToFileFormat();

			AudioFileInfo audioFileInfo = this.AudioFileInfoFactory.CreateAudio(request.FileName, inputFormat, request.InputStream);

			License license = this.Session.Linq<License>().SingleOrDefault(l => l.Abbreviation == request.License);
			
			AudioWork audioWork = new AudioWork(artist, Path.GetFileNameWithoutExtension(request.FileName), audioFileInfo, license, DateTime.Now);
			artist.AddSingleWork(audioWork);

			var response = this.CreateTypedResponse();
			response.Title = audioWork.Title.Value;
			return response;
		}
	}
}