using MusicCompany.Common.ViewModel;
using PagedList;

namespace MusicCompany.Common.Queries
{
	public class ListAudioSinglesQuery : QueryRequestBase
	{
		public string ArtistIdentifier
		{
			get;
			set;
		}

	}

	public class ListAudioSinglesQueryResponse : QueryResponseBase
	{
		public IPagedList<AudioSingleSummaryView> Singles
		{
			get;
			set;
		}
	}
}