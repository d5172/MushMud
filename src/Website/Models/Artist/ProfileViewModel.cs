using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicCompany.Common.ViewModel;

namespace MusicCompany.Website.Models.Artist
{
	public class ProfileViewModel
	{
		public ArtistDetailView Artist
		{
			get;
			set;
		}

		public int ProfilePictureWidth
		{
			get;
			set;
		}
	}
}
