using NHibernate.Type;

namespace MusicCompany.Core.NHib.CustomTypes
{
	public class WorkTypeEnumString : EnumStringType
	{
		public WorkTypeEnumString() :
			base(typeof(WorkType), 50)
		{
		}
	}
}
