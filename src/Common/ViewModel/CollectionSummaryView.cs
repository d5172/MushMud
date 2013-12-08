using System;
using System.Collections.Generic;

namespace MusicCompany.Common.ViewModel
{
	public class CollectionSummaryView : TopLevelWorkSummaryView
	{
		public int TrackCount
		{
			get;
			set;
		}

		public IList<AudioTrackSummaryView> Tracks
		{
			get;
			set;
		}
	}
}
