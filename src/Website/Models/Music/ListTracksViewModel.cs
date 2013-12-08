using System.Collections.Generic;
using MusicCompany.Common.ViewModel;

namespace MusicCompany.Website.Models.Music
{
	public class ListTracksViewModel
	{
		public IEnumerable<AudioTrackSummaryView> Tracks
		{
			get;
			set;
		}
	}
}
