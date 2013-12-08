using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using MusicCompany.Core.Services;

namespace MusicCompany.Core.Factories
{
	#region Interface

	public interface IImageFileInfoFactory
	{
		/// <summary>
		/// Returns a new ImageFileInfo instance.
		/// </summary>
		ImageFileInfo CreateImage(string originalFileName, FileFormat inputFormat, Stream inputStream);

	}

	#endregion

	#region Implementation

	public class ImageFileInfoFactory : IImageFileInfoFactory
	{
		#region Properties

		public IImageProcessingService ImageConversionService
		{
			get;
			set;
		}

		#endregion

		public ImageFileInfo CreateImage(string originalFileName, FileFormat inputFormat, Stream inputStream)
		{
			if (!inputFormat.IsImageType())
			{
				throw new InvalidOperationException(string.Format("{0} is not a supported image format.", inputFormat));
			}
			
			Size size = this.ImageConversionService.GetSize(inputStream);
			string colorMode = string.Empty; //TODO
			byte[] fileBytes = inputStream.GetBytes();
			BinaryFileData binaryFileData = new BinaryFileData(fileBytes);
			ImageFileInfo imageFile = new ImageFileInfo(Path.GetFileName(originalFileName), inputFormat, fileBytes.Length, binaryFileData, size.Width, size.Height, colorMode);
			return imageFile;
		}

	}
	
	#endregion
}