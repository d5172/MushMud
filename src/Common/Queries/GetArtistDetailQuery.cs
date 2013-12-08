using MusicCompany.Common.ViewModel;

namespace MusicCompany.Common.Queries
{
	public class GetArtistDetailQuery : QueryRequestBase
	{
		public string Identifier
		{
			get;
			set;
		}
	}

	public class GetArtistDetailResponse : QueryResponseBase
	{
		public ArtistDetailView Artist
		{
			get;
			set;
		}
	}
}
