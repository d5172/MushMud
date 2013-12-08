using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MusicCompany.Common.Commands;

namespace MusicCompany.Website.Models.AudioWork
{
	public class EditTrackViewModel
	{
		public UpdateTrackCommand Command
		{
			get;
			set;
		}
	}
}
