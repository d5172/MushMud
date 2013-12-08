using System;
using System.Collections.Generic;
using System.Linq;
using MusicCompany.Core.Services;

namespace MusicCompany.Core
{
	public class Work : VersionedEntity
	{
		#region Constructors

		protected Work()
		{
		}
		
		internal Work(Artist artist, string title, WorkType workType, License license, DateTime releaseDate) : this()
		{
			if (artist == null)
			{
				throw new ArgumentNullException("artist");
			}
			
			if (!string.IsNullOrEmpty(title))
			{
				this.Title = new UniqueName(title);
			}
			else
			{
				this.Title = new UniqueName("Untitled");
			}
			if (license == null)
			{
				throw new ArgumentNullException("license");
			}
			this.Artist = artist;
			this.WorkType = workType;
			this.Description = "";
			this.ReleaseDate = releaseDate;
			this.License = license;
			this.DateLicensed = DateTime.Now;
			this.Tags = new List<Tag>();
			this.Events = new List<WorkEvent>();
			this.LicenseHistory = new List<HistoricWorkLicense>();
		}

		#endregion

		#region Properties

		public virtual WorkType WorkType
		{
			get;
			protected set;
		}

		public virtual UniqueName Title
		{
			get;
			protected set;
		}

		public virtual DateTime ReleaseDate
		{
			get;
			protected internal set;
		}

		public virtual string Description
		{
			get;
			set;
		}

		public virtual License License
		{
			get;
			protected set;
		}

		public virtual DateTime DateLicensed
		{
			get;
			protected set;
		}

		public virtual CollectionWork ParentWork
		{
			get;
			protected internal set;
		}

		public virtual int ViewOrder
		{
			get;
			protected set;
		}

		public virtual Artist Artist
		{
			get;
			protected set;
		}

		protected virtual IList<Tag> Tags
		{
			get;
			set;
		}

		protected virtual IList<HistoricWorkLicense> LicenseHistory
		{
			get;
			set;
		}

		protected virtual IList<WorkEvent> Events
		{
			get;
			set;
		}

		public virtual bool CanChangeLicense
		{
			get
			{
				return (this.ParentWork == null);
			}
		}

		public virtual bool CanChangeReleaseDate
		{
			get
			{
				return (this.ParentWork == null);
			}
		}

		#endregion

		#region Public Methods

		public virtual IEnumerable<Tag> ListTags()
		{
			return new List<Tag>(this.Tags).AsReadOnly();
		}

		public virtual IEnumerable<HistoricWorkLicense> ListLicenseHistory()
		{
			return new List<HistoricWorkLicense>(this.LicenseHistory).AsReadOnly();
		}

		public virtual IList<DownloadEvent> ListDownloadEvents()
		{
			return this.Events.Where(e => e is DownloadEvent).Cast<DownloadEvent>().ToList().AsReadOnly();
		}

		public virtual IList<PlayEvent> ListPlayEvents()
		{
			return this.Events.Where(w => w is PlayEvent).Cast<PlayEvent>().ToList().AsReadOnly();
		}

		public virtual void LogDownloadEvent(Person person)
		{
			if ( person != null && this.Artist.IsManagedBy(person) )
			{
				return;
			}
			this.AddEvent(new DownloadEvent(this, person, DateTime.Now));
		}

		public virtual void LogCommentEvent(Person person)
		{
			this.AddEvent(new CommentEvent(this, person, DateTime.Now));
		}

		public virtual void SetTags(string[] newLemmas, ITagService tagService)
		{
			tagService.SetTags(this.Tags, newLemmas);
		}

		public virtual void ChangeLicense(License newLicense)
		{
			if (newLicense == null)
			{
				throw new ArgumentNullException("newLicense");
			}
			this.LicenseHistory.Add(new HistoricWorkLicense(this, this.License, this.DateLicensed, DateTime.Now));
			this.License = newLicense;
			this.DateLicensed = DateTime.Now;
		}

		public virtual void ChangeReleaseDate(DateTime releaseDate)
		{
			if (!this.CanChangeLicense)
			{
				throw new DomainLogicException("Release Date cannot be changed ona work that is part of a collection");
			}
			this.ReleaseDate = releaseDate;
		}

		public virtual string ToFileName(string extension)
		{
			if (!string.IsNullOrEmpty(extension) && !extension.StartsWith("."))
			{
				extension = "." + extension;
			}
			string fileName;
			if (this.WorkType == WorkType.Collection)
			{
				fileName = string.Format("{0} - {1}{2}", this.Artist.Name.ToSafePathName(), this.Title.ToSafePathName(), extension);
			}
			else if (this.ParentWork != null)
			{
				int trackNumber = this.ViewOrder + 1;
				string trackString = trackNumber.ToString().PadLeft(this.ParentWork.GetWorks().Count() < 100 ? 2 : 3, '0');
				fileName = string.Format("{0} - {1} - {2} - {3}{4}", this.Artist.Name.ToSafePathName(), this.ParentWork.Title.ToSafePathName(), trackString, this.Title.ToSafePathName(), extension);
			}
			else
			{
				fileName = string.Format("{0} - {1}{2}", this.Artist.Name.ToSafePathName(), this.Title.ToSafePathName(), extension);
			}
			return fileName;
		}

		#endregion

		#region Internal Methods

		protected internal virtual void AddEvent(WorkEvent workEvent)
		{
			this.Events.Add(workEvent);
		}

		protected internal virtual void ChangeTitle(string newTitle, IEnumerable<Work> siblings)
		{
			this.Title.Change(newTitle);
			this.CorrectTitle(siblings);
		}

		protected internal virtual void CorrectTitle(IEnumerable<Work> siblings)
		{
			if (siblings.Count(w => w.Title.Value == this.Title.Value && w != this) > 0)
			{
				int x = 0;
				string rolledTitle = this.Title.Value;
				while (siblings.Count(w => w.Title.Value == rolledTitle) > 0)
				{
					rolledTitle = string.Format("{0} {1}", this.Title.Value, ++x);
				}
				this.Title.Change(rolledTitle);
			}
		}

		protected internal virtual void CorrectViewOrder(IEnumerable<Work> siblings)
		{
			if ( siblings != null && siblings.Count() > 0)
			{
				this.ViewOrder = siblings.Max(w => w.ViewOrder) + 1;
			}
			else
			{
				this.ViewOrder = 0;
			}
		}

		protected internal virtual void MoveUpWithin(IEnumerable<Work> works)
		{
			if (this.ViewOrder == 0)
			{
				throw new InvalidOperationException("Work already at top");
			}
			works.Where(w => w.ViewOrder == this.ViewOrder - 1).Single().ViewOrder++;
			this.ViewOrder--;
		}

		protected internal virtual void MoveDownWithin(IEnumerable<Work> works)
		{
			if (this.ViewOrder == works.Max(w => w.ViewOrder))
			{
				throw new InvalidOperationException("Work already at bottom");
			}
			works.Where(w => w.ViewOrder == this.ViewOrder + 1).Single().ViewOrder--;
			this.ViewOrder++;
		}

		protected internal virtual void Leave(IEnumerable<Work> works)
		{
			foreach (Work sibling in works.Where(w => w.ViewOrder > this.ViewOrder))
			{
				sibling.ViewOrder--;
			}
			//this.ViewOrder = 0;
		}

		protected internal virtual void SetViewOrder(int viewOrder)
		{
			this.ViewOrder = viewOrder;
		}

		#endregion
	}
}