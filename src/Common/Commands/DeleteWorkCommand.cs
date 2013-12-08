
namespace MusicCompany.Common.Commands
{
	public class DeleteWorkCommand : CommandRequestBase
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

	public class DeleteWorkCommandResponse : CommandResponseBase
	{
		public string WorkTitle
		{
			get;
			set;
		}
	}
}