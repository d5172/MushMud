
using System;
namespace MusicCompany.Common.Commands
{
	public class UpdateAudioSingleCommand : CommandRequestBase
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

		public DateTime ReleaseDate
		{
			get;
			set;
		}

		public string LicenseIdentifier
		{
			get;
			set;
		}
	}

	public class UpdateAudioSingleCommandResponse : CommandResponseBase
	{
		public string Title
		{
			get;
			set;
		}
	}
}