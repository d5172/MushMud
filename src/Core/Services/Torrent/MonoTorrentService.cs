using System.Collections.Generic;
using System.IO;
using MonoTorrent.Common;
using MusicCompany.Infrastructure;

namespace MusicCompany.Core.Services.Torrent
{
	public class MonoTorrentService : ITorrentFileService
	{

		public string AnnounceUrl
		{
			get;
			set;
		}

		public IFilePath SourcePath
		{
			get;
			set;
		}

		public string Publisher
		{
			get;
			set;
		}

		public string PublisherUrl
		{
			get;
			set;
		}

		public string Comment
		{
			get;
			set;
		}

		public string CreatedBy
		{
			get;
			set;
		}

		public byte[] CreateTorrentFile(CollectionWork collectionWork)
		{
			//put files into sourcePath if they don't exist
			string sourcePath = Path.Combine(this.SourcePath.GetPath() ?? "", collectionWork.Title.ToSafePathName());
			this.PutFilesOnDisk(sourcePath, collectionWork);

			//New Creator
			TorrentCreator creator = new TorrentCreator();

			//add properties
			List<string> announces = new List<string>();
			announces.Add(this.AnnounceUrl);
			creator.Announces.Add(announces);

			creator.Comment = this.Comment;

			creator.CreatedBy = CreatedBy;

			creator.Publisher = this.Publisher;
			creator.PublisherUrl = this.PublisherUrl;
			creator.StoreMD5 = true;
			creator.Path = sourcePath;

			//create result
			byte[] output;
			using (MemoryStream stream = new MemoryStream())
			{
				creator.Create(stream);
				output = stream.ToArray();
			}
			return output;
		}

		private void PutFilesOnDisk(string path, CollectionWork collectionWork)
		{
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
				foreach (Work work in collectionWork.GetWorks())
				{
					if (work.WorkType == WorkType.Audio)
					{
						AudioWork audioWork = work as AudioWork;
						string fileName = audioWork.ToFileName(audioWork.File.FileFormat.GetExtension());
						string filePath = Path.Combine(path, fileName);
						this.WriteFileToDisk(audioWork.File, filePath);
						if (audioWork.File.AlternateFile != null)
						{
							fileName = audioWork.ToFileName(audioWork.File.AlternateFile.FileFormat.GetExtension());
							filePath = Path.Combine(path, fileName);
							this.WriteFileToDisk(audioWork.File.AlternateFile, filePath);
						}
					}
				}
			}
		}

		private void WriteFileToDisk(BinaryFileInfo binaryFileInfo, string filePath)
		{
			using (var fileStream = new FileStream(filePath, FileMode.Create))
			{
				fileStream.Write(binaryFileInfo.BinaryFileData.Data, 0, (int)binaryFileInfo.ByteCount);
				fileStream.Close();
			}
		}
	}
}
