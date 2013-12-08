using System.Collections.Generic;
using System;

namespace MusicCompany.Common.ViewModel
{
	public class ArtistWorkDetailView
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

		public IEnumerable<CollectionSummaryView> Collections
		{
			get;
			set;
		}

		public IEnumerable<AudioSingleSummaryView> Singles
		{
			get;
			set;
		}
	}
}
