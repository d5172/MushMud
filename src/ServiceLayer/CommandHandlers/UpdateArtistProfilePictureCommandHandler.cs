using System;
using Agatha.Common;
using MusicCompany.Common.Commands;
using MusicCompany.Core;
using MusicCompany.Core.Factories;
using MusicCompany.ServiceLayer.Base;
using System.IO;

namespace MusicCompany.ServiceLayer.CommandHandlers
{
	public class UpdateArtistProfilePictureCommandHandler : CommandHandler<UpdateArtistProfilePictureCommand, UpdateArtistProfilePictureCommandResponse>
	{
		public IImageFileInfoFactory ImageFileInfoFactory
		{
			get;
			set;
		}

		public override Response Handle(UpdateArtistProfilePictureCommand request)
		{
			Artist artist = this.GetArtistByIdentifier(request.ArtistIdentifier);
			this.CheckArtistManagementPermissions(request, artist);
			FileFormat fileFormat = Path.GetExtension(request.FileName).ToFileFormat();
			ImageFileInfo imageFileInfo = this.ImageFileInfoFactory.CreateImage(request.FileName, fileFormat, request.InputStream);
			artist.SetProfilePicture(imageFileInfo);

			this.Session.Transaction.Commit();

			var response =  this.CreateTypedResponse();
			response.NewImageId = imageFileInfo.Id;
			return response;
		}
	}
}