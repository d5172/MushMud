using System.IO;
using Agatha.Common;
using MusicCompany.Common.Commands;
using MusicCompany.Core;
using MusicCompany.Core.Factories;
using MusicCompany.ServiceLayer.Base;

namespace MusicCompany.ServiceLayer.CommandHandlers
{
	public class UpdateCollectionImageCommandHandler : CommandHandler<UpdateCollectionImageCommand, UpdateCollectionImageCommandResponse>
	{
		public IImageFileInfoFactory ImageFileInfoFactory
		{
			get;
			set;
		}

		public override Response Handle(UpdateCollectionImageCommand request)
		{
			var artist = this.GetArtistByIdentifier(request.ArtistIdentifier);
			this.CheckArtistManagementPermissions(request, artist);
			var collection = artist.GetCollectionWork(request.CollectionIdentifier);
			FileFormat fileFormat = Path.GetExtension(request.FileName).ToFileFormat();
			ImageFileInfo imageFileInfo = this.ImageFileInfoFactory.CreateImage(request.FileName, fileFormat, request.InputStream);
			collection.SetImageFile(imageFileInfo);
			
			this.Session.Transaction.Commit();

			var response = this.CreateTypedResponse();
			response.NewImageId = imageFileInfo.Id;
			return response;
		}
	}
}