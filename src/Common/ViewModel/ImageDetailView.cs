using System;

namespace MusicCompany.Common.ViewModel
{
	public class ImageDetailView
	{
		public Guid Id
		{
			get;
			set;
		}

		public string FileFormat
		{
			get;
			set;
		}

		public Byte[] Data
		{
			get;
			set;
		}

		public int Width
		{
			get;
			set;
		}

		public string OriginalFileName
		{
			get;
			set;
		}
	}
}