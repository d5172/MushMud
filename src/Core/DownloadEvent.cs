using System;

namespace MusicCompany.Core
{
	public class DownloadEvent : WorkEvent
	{
		protected DownloadEvent()
		{
		}

		internal DownloadEvent(Work work, Person person, DateTime eventDate)
			: base(work, person, eventDate, DomainEventType.Download)
		{
		}
	}
}