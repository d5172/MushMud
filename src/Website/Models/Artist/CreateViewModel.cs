using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicCompany.Common.Commands;

namespace MusicCompany.Website.Models.Artist
{
	public class CreateViewModel
	{
		public CreateArtistCommand Command
		{
			get;
			set;
		}
	}
}
