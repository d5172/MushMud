using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicCompany.Common.ViewModel;
using PagedList;

namespace MusicCompany.Website.Models.Works
{
	public class IndexViewModel
	{
		public string ArtistName
		{
			get;
			set;
		}

		public string ArtistIdentifier
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
