using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicCompany.Common.ViewModel;

namespace MusicCompany.Website.Models.YourMusic
{
	public class IndexViewModel
	{
		public IEnumerable<ArtistPersonSummaryView> Artists
		{
			get;
			set;
		}
	}
}
