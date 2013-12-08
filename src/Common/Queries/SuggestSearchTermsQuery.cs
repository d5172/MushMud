
namespace MusicCompany.Common.Queries
{
	public class SuggestSearchTermsQuery : QueryRequestBase
	{
		public string StartingWith
		{
			get;
			set;
		}
	}

	public class SuggestSearchTermsQueryResponse : QueryResponseBase
	{
		public string[] SuggestedTerms
		{
			get;
			set;
		}
	}
}
