using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicCompany.Common.Commands;

namespace MusicCompany.Website.Models.Artist
{
	public class BioFormViewModel
	{
		public string ArtistName
		{
			get;
			set;
		}

		public UpdateArtistProfileCommand Command
		{
			get;
			set;
		}
	}
}
