
namespace MusicCompany.Common.Commands
{
	public class DeleteArtistCommand : CommandRequestBase
	{
		public string Identifier
		{
			get;
			set;
		}
	}

	public class DeleteArtistCommandResponse : CommandResponseBase
	{
		public string ArtistName
		{
			get;
			set;
		}
	}
}