using System;

namespace MusicCompany.Common.ViewModel
{
	public class AudioTrackSummaryView
	{
		public int ViewOrder
		{
			get;
			set;
		}
		public string Identifier
		{
			get;
			set;
		}
		public string Title
		{
			get;
			set;
		}
		public string CollectionIdentifier
		{
			get;
			set;
		}
		public string ArtistIdentifier
		{
			get;
			set;
		}
		public string Description
		{
			get;
			set;
		}
		public string Tags
		{
			get;
			set;
		}
		public int Seconds
		{
			get;
			set;
		}
		public int DownloadCount
		{
			get;
			set;
		}
		public int PlayCount
		{
			get;
			set;
		}
		public Guid BinaryFileId
		{
			get;
			set;
		}
		public string FileFormat
		{
			get;
			set;
		}
		public string AlternateFileFormat
		{
			get;
			set;
		}
	}
}