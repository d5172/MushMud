using System;

namespace MusicCompany.Core
{
	public class PlayEvent : WorkEvent
	{
		protected PlayEvent()
		{
		}

		internal PlayEvent(Work work, Person person, DateTime eventDate)
			: base(work, person, eventDate, DomainEventType.Play)
		{
		}
	}
}