using System;

namespace MusicCompany.Core
{
	public abstract class DomainEvent : Entity
	{
		protected DomainEvent()
		{
		}

		internal DomainEvent(Person person, DateTime eventDate, DomainEventType type)
		{
			this.Person = person;
			this.EventDate = eventDate;
			this.DomainEventType = type;
		}

		public virtual DomainEventType DomainEventType
		{
			get;
			protected set;
		}

		public virtual DateTime EventDate
		{
			get;
			protected set;
		}

		public virtual Person Person
		{
			get;
			protected set;
		}
	}
}
