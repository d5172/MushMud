using MusicCompany.Core;
using MusicCompany.Common.ViewModel;

namespace MusicCompany.Website.Models.Music
{
	public class DownloadFormViewModel
	{
		public string WorkTitle
		{
			get;
			set;
		}

		public string ArtistName
		{
			get;
			set;
		}

		public string ArtistIdentifier
		{
			get;
			set;
		}

		public LicenseDetailView License
		{
			get;
			set;
		}

		public string FileFormat
		{
			get;
			set;
		}

		public string FormAction
		{
			get;
			set;
		}

		public string AlternateFormAction
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