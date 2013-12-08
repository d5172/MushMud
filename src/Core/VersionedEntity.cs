using System;

namespace MusicCompany.Core
{
	public class VersionedEntity : Entity
	{

		protected VersionedEntity()
		{
			this.Id = Guid.NewGuid();
		}

		public virtual DateTime Version
		{
			get;
			set;
		}
	}
}