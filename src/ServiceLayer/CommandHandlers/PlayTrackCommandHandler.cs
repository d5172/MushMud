using System;
using Agatha.Common;
using MusicCompany.Common.Commands;
using MusicCompany.Core;
using MusicCompany.ServiceLayer.Base;

namespace MusicCompany.ServiceLayer.CommandHandlers
{
	public class PlayTrackCommandHandler : CommandHandler<PlayTrackCommand, PlayTrackCommandResponse>
	{
		public override Response Handle(PlayTrackCommand request)
		{
			var artist = this.GetArtistByIdentifier(request.ArtistIdentifier);
			var collection = artist.GetCollectionWork(request.CollectionIdentifier);
			var track = collection.GetWork(request.WorkIdentifier) as AudioWork;
			var requestedFileFormat = (FileFormat) Enum.Parse(typeof(FileFormat), request.FileFormat);

			BinaryFileInfo fileInfo = track.GetBinaryFileInfoForRequestedFormat(requestedFileFormat);
			if ( fileInfo == null )
			{
				throw new Exception(string.Format("{0} is not available as {1}", track.Title, requestedFileFormat));
			}

			if ( !request.SkipEventLogging )
			{
				var person = this.GetPersonFromCommandContext(request);
				track.LogPlayEvent(person);
			}

			var response = this.CreateTypedResponse();
			response.FileName = track.ToFileName(requestedFileFormat.GetExtension());
			response.MimeType = requestedFileFormat.ToMimeType();
			response.Data = fileInfo.BinaryFileData.Data;
			return response;
		}
	}
}