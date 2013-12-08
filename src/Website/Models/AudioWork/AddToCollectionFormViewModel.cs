using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicCompany.Common.Commands;
using MusicCompany.Common.ViewModel;
using System.Web.Mvc;

namespace MusicCompany.Website.Models.AudioWork
{
	public class AddToCollectionFormViewModel
	{
		public string ArtistId
		{
			get;
			set;
		}

		public string WorkId
		{
			get;
			set;
		}

		public SelectList AvailableCollections
		{
			get;
			set;
		}
	}
}
