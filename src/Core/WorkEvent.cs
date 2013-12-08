using System;

namespace MusicCompany.Core
{
	public class WorkEvent : DomainEvent
	{
		protected WorkEvent()
		{
		}

		internal WorkEvent(Work work, Person person, DateTime eventDate, DomainEventType type )
			: base(person, eventDate, type)
		{
			this.Work = work;
		}

		public virtual Work Work
		{
			get;
			protected set;
		}
	}
}
