using NHibernate.Type;

namespace MusicCompany.Core.NHib.CustomTypes
{
	public class FileFormatEnumString : EnumStringType
	{
		public FileFormatEnumString() :
			base(typeof(FileFormat), 50)
		{
		}
	}
}
