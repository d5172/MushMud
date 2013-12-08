
namespace MusicCompany.Common.Commands
{
	public class CreatePersonCommand : CommandRequestBase
	{
		public string Name
		{
			get;
			set;
		}

		public string Username
		{
			get;
			set;
		}

		public string ArtistName
		{
			get;
			set;
		}
	}

	public class CreatePersonCommandResponse : CommandResponseBase
	{
		public string PersonName
		{
			get;
			set;
		}
	}
}