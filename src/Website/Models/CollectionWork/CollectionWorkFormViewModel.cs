using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicCompany.Common.Commands;

namespace MusicCompany.Website.Models.CollectionWork
{
	public class CollectionWorkFormViewModel
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

		public CollectionCommandBase Command
		{
			get;
			set;
		}

		public SelectList AvailableLicenses
		{
			get;
			set;
		}

		public string FormAction
		{
			get;
			set;
		}
	}
}
