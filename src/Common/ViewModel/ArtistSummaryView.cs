using System;

namespace MusicCompany.Common.ViewModel
{
	public class ArtistSummaryView
	{
		public string Identifier
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public Guid ProfileImageId
		{
			get;
			set;
		}

		public int CollectionCount
		{
			get;
			set;
		}

		public int SingleCount
		{
			get;
			set;
		}
	}
}
