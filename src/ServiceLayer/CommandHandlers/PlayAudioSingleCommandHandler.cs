using System;
using Agatha.Common;
using MusicCompany.Common.Commands;
using MusicCompany.Core;
using MusicCompany.ServiceLayer.Base;

namespace MusicCompany.ServiceLayer.CommandHandlers
{
	public class PlayAudioSingleCommandHandler : CommandHandler<PlayAudioSingleCommand, PlayAudioSingleCommandResponse>
	{
		public override Response Handle(PlayAudioSingleCommand request)
		{
			var artist = this.GetArtistByIdentifier(request.ArtistIdentifier);
			var work = artist.GetSingleWork(request.WorkIdentifier) as AudioWork;
			var requestedFileFormat = (FileFormat) Enum.Parse(typeof(FileFormat), request.FileFormat);

			BinaryFileInfo fileInfo = work.GetBinaryFileInfoForRequestedFormat(requestedFileFormat);
			if ( fileInfo == null )
			{
				throw new Exception(string.Format("{0} is not available as {1}", work.Title, requestedFileFormat));
			}

			if ( !request.SkipEventLogging )
			{
				var person = this.GetPersonFromCommandContext(request);
				work.LogPlayEvent(person);
			}

			var response = this.CreateTypedResponse();
			response.FileName = work.ToFileName(requestedFileFormat.GetExtension());
			response.MimeType = requestedFileFormat.ToMimeType();
			response.Data = fileInfo.BinaryFileData.Data;
			return response;
		}
	}
}