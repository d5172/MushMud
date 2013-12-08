using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicCompany.Common.ViewModel
{
	public class ArtistPersonSummaryView
	{
		public string Identifier
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public Guid ProfileImageId
		{
			get;
			set;
		}

		public string Username
		{
			get;
			set;
		}

		public int CollectionCount
		{
			get;
			set;
		}

		public int SingleCount
		{
			get;
			set;
		}
	}
}
