using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagedList;
using MusicCompany.Common.ViewModel;

namespace MusicCompany.Common.Queries
{
	public class ListTracksQuery : QueryRequestBase
	{
		public string ArtistIdentifier
		{
			get;
			set;
		}

		public string CollectionIdentifier
		{
			get;
			set;
		}
	}


	public class ListTracksQueryResponse : QueryResponseBase
	{
		public IEnumerable<AudioTrackSummaryView> Tracks
		{
			get;
			set;
		}
	}
}
