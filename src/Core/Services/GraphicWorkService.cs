using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MusicCompany.Core.Services
{
	public interface IGraphicWorkService
	{
		BinaryFileInfo CreateFullResImage(string fileName, string mimeType, Stream dataStream);
		BinaryFileInfo CreateLowResImage(string fileName, string mimeType, Stream dataStream);
	}

	internal class GraphicWorkService : WorkServiceBase,  IGraphicWorkService
	{

		public BinaryFileInfo CreateFullResImage(string fileName, string mimeType, Stream dataStream)
		{
			byte[] bytes = this.GetFileBytes(dataStream);
			BinaryFileData data = new BinaryFileData(bytes);
			BinaryFileInfo info = new BinaryFileInfo(fileName, mimeType, bytes.Length, data);
			return info;
		}

		public BinaryFileInfo CreateLowResImage(string fileName, string mimeType, Stream dataStream)
		{
			//TODO: resample to lower res if big
			byte[] convertedBytes = new byte[0];

			BinaryFileData fileData =new BinaryFileData(convertedBytes);
			BinaryFileInfo fileInfo = new BinaryFileInfo(fileName, mimeType, convertedBytes.Length, fileData);

			return fileInfo;
		}

	
	}
}
