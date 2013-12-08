using System;

namespace MusicCompany.Core
{
	public class CommentEvent : WorkEvent
	{
		protected CommentEvent()
		{
		}

		internal CommentEvent(Work work, Person person, DateTime eventDate)
			: base(work, person, eventDate, DomainEventType.Comment)
		{
		}
	}
}