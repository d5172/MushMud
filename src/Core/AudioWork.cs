using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MusicCompany.Core
{
	public class AudioWork : Work
	{
		#region Constructors

		protected AudioWork()
		{
		}

		public AudioWork(Artist artist, string title, AudioFileInfo audioFile, License license, DateTime releaseDate)
			: base(artist, title, WorkType.Audio, license, releaseDate) 
		{
			if (audioFile == null)
			{
				throw new ArgumentNullException("audioFile");
			}
			this.File = audioFile;
			if (string.IsNullOrEmpty(this.Title.Value))
			{
				this.Title = new UniqueName(Path.GetFileNameWithoutExtension(this.File.OriginalFileName));
			}
		}

		#endregion

		#region Properties

		public virtual AudioFileInfo File
		{
			get;
			protected set;
		}

		public virtual TimeSpan Duration
		{
			get
			{
				if (this.File != null)
				{
					return TimeSpan.FromSeconds(this.File.Seconds);
				}
				else
				{
					return TimeSpan.Zero;
				}
			}
		}

		public virtual int TrackNumber
		{
			get
			{
				return this.ViewOrder + 1;
			}
		}

		#endregion

		#region Public Methods

		public virtual void LogPlayEvent(Person person)
		{
			if ( person != null && this.Artist.IsManagedBy(person) )
			{
				return;
			}
			this.AddEvent(new PlayEvent(this, person, DateTime.Now));
		}

		public virtual BinaryFileInfo GetBinaryFileInfoForRequestedFormat(FileFormat requestedFileFormat)
		{
			if ( this.File.FileFormat == requestedFileFormat )
			{
				return this.File;
			}
			else if ( this.File.AlternateFile != null && this.File.AlternateFile.FileFormat == requestedFileFormat )
			{
				return this.File.AlternateFile;
			}
			return null;
		}

		#endregion
	}
}
