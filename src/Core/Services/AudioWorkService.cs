using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MusicCompany.Core.Repositories;

namespace MusicCompany.Core.Services
{
	public interface IAudioWorkService
	{
		void CreateAudioWork(Artist artist, Work collection, string title, string description, string originalFileName, Stream dataStream);
	}

	internal class AudioWorkService : WorkServiceBase, IAudioWorkService
	{
		public void CreateAudioWork(Artist artist, Work collection, string title, string description, string originalFileName, Stream dataStream)
		{
			var work = new Work(artist, title, WorkType.Audio);
			if (!string.IsNullOrEmpty(description))
			{
				work.Description = description;
			}
			
			if(collection != null)
			{
				work.Collection = collection;
				work.ViewOrder = this.GetNextViewOrder(collection);
			}

			string fileName = Path.GetFileNameWithoutExtension(originalFileName);

			byte[] fileBytes = this.GetFileBytes(dataStream);
			work.PrimaryFile = this.CreateFlacFile(fileName, fileBytes);
			work.SecondaryFile = this.CreateMP3File(fileName, fileBytes);

			work.Seconds = this.GetSeconds(dataStream);

			this.WorkRepository.Save(work);
		}

		

		private int GetSeconds(Stream dataStream)
		{
			//TODO
			return 0;
		}

		

		private BinaryFileInfo CreateFlacFile(string title, byte[] fileBytes)
		{
			//TODO: if WAVE, compress to FLAC

			BinaryFileData fileData =new BinaryFileData(fileBytes);
			BinaryFileInfo fileInfo = new BinaryFileInfo(title + ".flac", "application/flac", fileBytes.Length, fileData);

			return fileInfo;
		}

		private BinaryFileInfo CreateMP3File(string title, byte[] fileBytes)
		{
			//TODO: convert to MP3
			byte[] convertedBytes = new byte[0];

			BinaryFileData fileData =new BinaryFileData(convertedBytes);
			BinaryFileInfo fileInfo = new BinaryFileInfo(title + ".mp3", "application/mp3", convertedBytes.Length, fileData);

			return fileInfo;
		}
	}
}
