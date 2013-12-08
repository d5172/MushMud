using System;

namespace MusicCompany.Core
{
	public class AudioFileInfo : BinaryFileInfo
	{
		#region Constructors

		protected AudioFileInfo() : base()
		{
		}

		public AudioFileInfo(string originalFileName, FileFormat format, long byteCount, BinaryFileData data, int seconds)
			: base(originalFileName, format, byteCount, data)
		{
			this.Seconds = seconds;
		}

		#endregion

		#region Properties

		public virtual int Seconds
		{
			get;
			protected set;
		}

		protected virtual AudioFileInfo Parent
		{
			get;
			set;
		}

		public virtual TimeSpan Duration
		{
			get
			{
				return TimeSpan.FromSeconds(this.Seconds);
			}
		}

		public virtual AudioFileInfo AlternateFile
		{
			get;
			protected set;
		}

		#endregion

		#region Public Methods

		public virtual void SetAlternateFile(AudioFileInfo alternateFile)
		{
			if (alternateFile == null)
			{
				throw new ArgumentNullException("alternateFile");
			}
			this.AlternateFile = alternateFile;
			alternateFile.Parent = this;
		}

		#endregion
	}
}
