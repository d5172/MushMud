using MusicCompany.Common.ViewModel;

namespace MusicCompany.Common.Queries
{
	public class GetArtistWorkDetailQuery : QueryRequestBase
	{
		public string Identifier
		{
			get;
			set;
		}
	}

	public class GetArtistWorkDetailQueryResponse : QueryResponseBase
	{
		public ArtistWorkDetailView Artist
		{
			get;
			set;
		}
	}
}
