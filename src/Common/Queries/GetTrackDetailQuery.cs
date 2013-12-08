using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagedList;
using MusicCompany.Common.ViewModel;

namespace MusicCompany.Common.Queries
{
	public class GetTrackDetailQuery : QueryRequestBase
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

		public string WorkIdentifier
		{
			get;
			set;
		}
	}

	public class GetTrackDetailQueryResponse : QueryResponseBase
	{
		public AudioTrackSummaryView Track
		{
			get;
			set;
		}
	}
}
