using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagedList;
using MusicCompany.Common.ViewModel;

namespace MusicCompany.Common.Queries
{
	public class ArtistNameAvailableQuery : QueryRequestBase
	{
		public string PotentialName
		{
			get;
			set;
		}

		public string ExcludeIdentifier
		{
			get;
			set;
		}
	}

	public class ArtistNameAvailableResponse : QueryResponseBase
	{
		public bool IsAvailable
		{
			get;
			set;
		}
	}
}
