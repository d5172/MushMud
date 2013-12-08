using NHibernate.Type;

namespace MusicCompany.Core.NHib.CustomTypes
{
	public class DomainEventTypeEnumString : EnumStringType
	{
		public DomainEventTypeEnumString() :
			base(typeof(DomainEventType), 50)
		{
		}
	}
}
