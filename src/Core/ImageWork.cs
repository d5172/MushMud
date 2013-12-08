using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicCompany.Core
{
	public class ImageWork : Work
	{
		protected ImageWork() 
		{
		}

		internal ImageWork(Artist artist, string title, ImageFileInfo imageFile, License license, DateTime releaseDate)
			: base(artist, title, WorkType.Graphic, license, releaseDate) 
		{
			if (imageFile == null)
			{
				throw new ArgumentNullException("imageFile");
			}
			this.File = imageFile;
		}
		
		public virtual ImageFileInfo File
		{
			get;
			protected set;
		}
	}
}
