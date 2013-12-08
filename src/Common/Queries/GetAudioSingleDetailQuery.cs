using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagedList;
using MusicCompany.Common.ViewModel;

namespace MusicCompany.Common.Queries
{
	public class GetAudioSingleDetailQuery : QueryRequestBase
	{
		public string ArtistIdentifier
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

	public class GetAudioSingleDetailQueryResponse : QueryResponseBase
	{
		public AudioSingleSummaryView Single
		{
			get;
			set;
		}
	}
}
