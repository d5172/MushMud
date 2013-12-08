using System;

namespace MusicCompany.Core
{
	public class BinaryFileInfo : Entity
	{
		#region Constructors

		protected BinaryFileInfo()
		{
		}

		public BinaryFileInfo(string originalFileName, FileFormat format, long byteCount, BinaryFileData data)
		{
			if (string.IsNullOrEmpty(originalFileName))
			{
				throw new DomainLogicException("OriginalFileName cannot be blank");
			}
			this.OriginalFileName = originalFileName;
			this.FileFormat = format;
			this.ByteCount = byteCount;
			if (data == null)
			{
				throw new ArgumentNullException("data");
			}
			this.BinaryFileData = data;
		}

		#endregion

		#region Properties

		public virtual string OriginalFileName
		{
			get;
			protected set;
		}

		public virtual FileFormat FileFormat
		{
			get;
			protected set;
		}

		public virtual long ByteCount
		{
			get;
			protected set;
		}

		public virtual BinaryFileData BinaryFileData
		{
			get;
			protected set;
		}

		#endregion
	}
}