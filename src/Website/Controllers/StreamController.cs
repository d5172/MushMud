using System;
using System.Web.Mvc;
using MusicCompany.Common.Commands;

namespace MusicCompany.Website.Controllers
{
	public class StreamController : ExtendedController
	{
		#region Checking for duplicate requests

		private static readonly int duplicateRequestThresholdSeconds = 5;

		/// <summary>
		/// This returns true if the user has previously requested the same stream
		/// within the duplicateRequestThresholdSeconds.
		/// This is used to tell the command to skip the event logging, to avoid 
		/// having extraneous play events the log.
		/// </summary>
		/// <remarks>
		/// Webkit browsers (Chrome, Safari) will request the file twice
		/// when using html5 audio tag.  This should handle that annoyance as well.
		/// </remarks>
		private bool IsProbableDuplicateRequest(string key)
		{
			bool isProbableDuplicate = false;
			if ( Session[key] != null )
			{
				DateTime lastPlay = (DateTime) Session[key];
				TimeSpan elapsed = DateTime.Now.Subtract(lastPlay);
				if ( elapsed.TotalSeconds < duplicateRequestThresholdSeconds )
				{
					isProbableDuplicate = true;
				}
			}
			Session[key] = DateTime.Now;
			return isProbableDuplicate;
		}
		
		private bool IsProbableDuplicateRequest(string artistId, string workId, string collectionId)
		{
			string key = string.Concat(artistId, workId, collectionId);
			return IsProbableDuplicateRequest(key);
		}

		private bool IsProbableDuplicateRequest(string artistId, string workId)
		{
			string key = string.Concat(artistId, workId);
			return IsProbableDuplicateRequest(key);
		}

		#endregion

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult PlayTrack(string artistId, string workId, string collectionId, string format)
		{
			var command = new PlayTrackCommand();
			command.ArtistIdentifier = artistId;
			command.CollectionIdentifier = collectionId;
			command.WorkIdentifier = workId;
			command.FileFormat = format;
			command.SkipEventLogging = this.IsProbableDuplicateRequest(artistId, workId, collectionId);

			var result = this.ProcessRequest<PlayTrackCommandResponse>(command);

			return File(result.Data, result.MimeType);
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult PlaySingle(string artistId, string workId, string format)
		{
			var command = new PlayAudioSingleCommand();
			command.ArtistIdentifier = artistId;
			command.WorkIdentifier = workId;
			command.FileFormat = format;
			command.SkipEventLogging = this.IsProbableDuplicateRequest(artistId, workId);

			var result = this.ProcessRequest<PlayAudioSingleCommandResponse>(command);

			return File(result.Data, result.MimeType);
		}
		
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Torrent(string artistId, string workId)
		{
			var command = new DownloadCollectionTorrentCommand();
			command.ArtistIdentifier = artistId;
			command.CollectionIdentifier = workId;

			var result = this.ProcessRequest<DownloadCollectionTorrentCommandResponse>(command);

			return File(result.Data, result.MimeType, result.FileName);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Zip(string artistId, string workId)
		{
			var command = new DownloadCollectionZipCommand();
			command.ArtistIdentifier = artistId;
			command.CollectionIdentifier = workId;

			var result = this.ProcessRequest<DownloadCollectionZipCommandResponse>(command);

			return File(result.Data, result.MimeType, result.FileName);
		}


		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult DownloadTrack(string artistId, string workId, string collectionId, string format)
		{
			var command = new DownloadTrackCommand();
			command.ArtistIdentifier = artistId;
			command.CollectionIdentifier = collectionId;
			command.WorkIdentifier = workId;
			command.FileFormat = format;

			var result = this.ProcessRequest<DownloadTrackCommandResponse>(command);

			return File(result.Data, result.MimeType, result.FileName);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult DownloadSingle(string artistId, string workId,  string format)
		{
			var command = new DownloadAudioSingleCommand();
			command.ArtistIdentifier = artistId;
			command.WorkIdentifier = workId;
			command.FileFormat = format;

			var result = this.ProcessRequest<DownloadAudioSingleCommandResponse>(command);

			return File(result.Data, result.MimeType, result.FileName);
		}
	}
}
