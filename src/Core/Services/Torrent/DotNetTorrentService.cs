using MusicCompany.Core;
using MusicCompany.Core.Services;
using DotNetTorrent.BEncoding;
using System.IO;

namespace MusicCompany.Core.Services.Torrent
{
	public class DotNetTorrentService : ITorrentFileService
	{
		public string AnnounceUrl
		{
			get;
			set;
		}

		public byte[] CreateTorrentFile(CollectionWork collectionWork)
		{
			BDictionary file = new BDictionary();
			file.Add("announce", AnnounceUrl);

			BDictionary info = new BDictionary();
			foreach (Work work in collectionWork.GetWorks())
			{
				//TODO, add an info for each work;
			}
			//TODO: add a text file of the license

			file.Add("info", info);

			byte[] output;
			using (MemoryStream stream = new MemoryStream())
			{
				DotNetTorrent.BEncoding.Torrent.SaveTorrent(file, stream);
				output = stream.ToArray();
			}
			return output;
		}
	}
}
