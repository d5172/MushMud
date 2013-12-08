
namespace MusicCompany.Common.Commands
{
	public class UpdateArtistProfileCommand : CommandRequestBase
	{
		public string Identifier
		{
			get;
			set;
		}

		public string Bio
		{
			get;
			set;
		}
	}

	public class UpdateArtistProfileCommandResponse : CommandResponseBase
	{

	}
}