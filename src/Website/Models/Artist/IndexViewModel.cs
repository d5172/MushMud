using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using MusicCompany.Common.ViewModel;

namespace MusicCompany.Website.Models.Artist
{
	public class IndexViewModel
	{
		public IPagedList<ArtistSummaryView> Artists
		{
			get;
			set;
		}
	}
}
