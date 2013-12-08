using System.Collections.Generic;
using System;

namespace MusicCompany.Common.ViewModel
{
	public class ArtistDetailView
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

		public string Bio
		{
			get;
			set;
		}

		public IEnumerable<TopLevelWorkSummaryView> TopLevelWorks
		{
			get;
			set;
		}
	}
}
