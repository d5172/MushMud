
namespace MusicCompany.Core
{
	public class BinaryFileData : Entity
	{
		#region Constructors

		protected BinaryFileData()
		{
		}

		public BinaryFileData(byte[] data)
			: this()
		{
			if (data.Length == 0)
			{
				throw new DomainLogicException("No data");
			}
			this.Data = data;
		}

		#endregion

		#region Properties

		public virtual byte[] Data
		{
			get;
			protected set;
		}

		#endregion
	}
}
