using System.IO;
using Ionic.Zip;
using Ionic.Zlib;

namespace MusicCompany.Core.Services.Zipping
{
	class DotNetZipFileService : IZipFileService
	{
		private static readonly CompressionLevel defaultCompressionLevel = CompressionLevel.None;
		
		#region IZipFileService Members

		public byte[] CreateZipFile(CollectionWork collectionWork)
		{
			using ( var zipFile = new ZipFile())
			{
				zipFile.CompressionLevel = defaultCompressionLevel;
				AddCollectionImage(collectionWork, zipFile);
				AddAudioWorks(collectionWork, zipFile);
				return SaveToByteArray(zipFile);
			}
		}

		#endregion

		private static void AddCollectionImage(CollectionWork collectionWork, ZipFile zipFile)
		{
			if ( collectionWork.File != null )
			{
				zipFile.AddEntry(collectionWork.ToFileName(collectionWork.File.FileFormat.GetExtension()), collectionWork.File.BinaryFileData.Data);
			}
		}

		private static void AddAudioWorks(CollectionWork collectionWork, ZipFile zipFile)
		{
			foreach ( var work in collectionWork.GetWorks() )
			{
				if ( work.WorkType == WorkType.Audio )
				{
					AudioWork audioWork = work as AudioWork;
					AddAudioFile(audioWork, zipFile);
				}
			}
		}

		private static void AddAudioFile(AudioWork audioWork, ZipFile zipFile)
		{
			string fileName = audioWork.ToFileName(audioWork.File.FileFormat.GetExtension());
			zipFile.AddEntry(fileName, audioWork.File.BinaryFileData.Data);
			if ( audioWork.File.AlternateFile != null )
			{
				fileName = audioWork.ToFileName(audioWork.File.AlternateFile.FileFormat.GetExtension());
				zipFile.AddEntry(fileName, audioWork.File.AlternateFile.BinaryFileData.Data);
			}
		}

		private static byte[] SaveToByteArray(ZipFile zipFile)
		{
			byte[] output;
			using ( var stream = new MemoryStream() )
			{
				zipFile.Save(stream);
				output = stream.ToArray();
			}
			return output;
		}
	}
}
