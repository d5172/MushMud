using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicCompany.Core
{
	public class ImageFileInfo : BinaryFileInfo
	{
		protected ImageFileInfo() : base()
		{
		}

		public ImageFileInfo(string originalFileName, FileFormat format, long byteCount, BinaryFileData data, int horizontalRes, int verticalRes, string colorMode) :
			base(originalFileName, format, byteCount, data)
		{
			this.HorizontalResolution = horizontalRes;
			this.VerticalResolution = verticalRes;
			this.ColorMode = colorMode;
		}
		
		public virtual int HorizontalResolution
		{
			get;
			protected set;
		}

		public virtual int VerticalResolution
		{
			get;
			protected set;
		}

		public virtual string ColorMode
		{
			get;
			protected set;
		}
	}
}
