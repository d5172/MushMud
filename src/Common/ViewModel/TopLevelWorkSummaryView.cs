using System;

namespace MusicCompany.Common.ViewModel
{
	public class TopLevelWorkSummaryView
	{
		public Guid Id
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
		public string ArtistIdentifier
		{
			get;
			set;
		}
		public string ArtistName
		{
			get;
			set;
		}
		public Guid ArtistImageId
		{
			get;
			set;
		}
		public string Description
		{
			get;
			set;
		}
		public DateTime ReleaseDate
		{
			get;
			set;
		}
		public LicenseDetailView License
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
		public int Rank
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
		public bool IsReleased
		{
			get;
			set;
		}
	}
}
