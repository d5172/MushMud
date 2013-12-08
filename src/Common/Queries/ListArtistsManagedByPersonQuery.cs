using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagedList;
using MusicCompany.Common.ViewModel;

namespace MusicCompany.Common.Queries
{
	public class ListArtistsManagedByPersonQuery : QueryRequestBase
	{
		public string Username
		{
			get;
			set;
		}
	}

	public class ListArtistsManagedByPersonQueryResponse : QueryResponseBase
	{
		public IPagedList<ArtistPersonSummaryView> Artists
		{
			get;
			set;
		}
	}
}
