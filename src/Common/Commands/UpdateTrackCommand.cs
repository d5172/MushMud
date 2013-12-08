
namespace MusicCompany.Common.Commands
{
	public class UpdateTrackCommand : CommandRequestBase
	{
		public string ArtistIdentifier
		{
			get;
			set;
		}

		public string CollectionIdentifier
		{
			get;
			set;
		}

		public string WorkIdentifier
		{
			get;
			set;
		}

		public string Title
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public string Tags
		{
			get;
			set;
		}
	}

	public class UpdateTrackCommandResponse : CommandResponseBase
	{
		public string Title
		{
			get;
			set;
		}
	}
}