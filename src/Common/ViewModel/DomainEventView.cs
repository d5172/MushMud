using System;

namespace MusicCompany.Common.ViewModel
{
	public class DomainEventView
	{
		public Guid Id
		{
			get;
			set;
		}
		public string Username
		{
			get;
			set;
		}
		public string DomainEventType
		{
			get;
			set;
		}
		public string EventUsername
		{
			get;
			set;
		}
		public string Title
		{
			get;
			set;
		}
		public string WorkIdentifier
		{
			get;
			set;
		}
		public string WorkType
		{
			get;
			set;
		}
		public string CollectionTitle
		{
			get;
			set;
		}
		public string CollectionIdentifier
		{
			get;
			set;
		}
		public string ArtistName
		{
			get;
			set;
		}
		public string ArtistIdentifier
		{
			get;
			set;
		}
		public  DateTime EventDate
		{
			get;
			set;
		}
	}
}
