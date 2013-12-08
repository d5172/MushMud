using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagedList;
using MusicCompany.Common.ViewModel;

namespace MusicCompany.Common.Queries
{
	public class ListArtistsQuery : QueryRequestBase
	{
	}

	public class ListArtistsResponse : QueryResponseBase
	{
		public IPagedList<ArtistSummaryView> Artists
		{
			get;
			set;
		}
	}
}
