using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicCompany.Common.Commands;
using System.Web.Mvc;

namespace MusicCompany.Website.Models.AudioWork
{
	public class EditSingleViewModel
	{
		public UpdateAudioSingleCommand Command
		{
			get;
			set;
		}

		public SelectList AvailableLicenses
		{
			get;
			set;
		}
	}
}
