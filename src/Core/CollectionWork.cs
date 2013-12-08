using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicCompany.Core
{
	public class CollectionWork : Work
	{
		#region Constructors

		protected CollectionWork()
		{
		}

		public CollectionWork(Artist artist, string title, License license, DateTime releaseDate)
			: base(artist, title, WorkType.Collection, license, releaseDate)
		{
			this.SubWorks = new List<Work>();
		}

		#endregion

		#region Properties

		protected virtual IList<Work> SubWorks
		{
			get;
			set;
		}

		public virtual ImageFileInfo File
		{
			get;
			protected set;
		}

		public virtual TimeSpan Duration
		{
			get
			{
				return TimeSpan.FromSeconds(
					this.SubWorks.Where(w => w.GetType() == typeof(AudioWork))
					.Sum(w => ((AudioWork)w).Duration.TotalSeconds)
					);
			}
		}

		#endregion

		#region Internal Methods

		protected internal virtual void AddWork(Work subWork)
		{
			if (subWork == null)
			{
				throw new ArgumentNullException("subWork");
			}
			subWork.CorrectTitle(this.SubWorks);
			subWork.CorrectViewOrder(this.SubWorks);
			subWork.ParentWork = this;
			if (subWork.License != this.License)
			{
				subWork.ChangeLicense(this.License);
			}
			this.SubWorks.Add(subWork);
		}

		protected internal virtual void RemoveWork(Work subWork)
		{
			if (subWork == null)
			{
				throw new ArgumentNullException("subWork");
			}
			subWork.Leave(this.SubWorks);
			//see Artist.AddWorkToParent for why we don't do this
			//this.SubWorks.Remove(subWork);
			subWork.ParentWork = null;
		}

		#endregion

		#region Public Methods

		public virtual IEnumerable<Work> GetWorks()
		{
			return this.SubWorks.OrderBy(w => w.ViewOrder);
		}

		public virtual Work GetWork(string titleId)
		{
			return this.SubWorks.Single(w => w.Title.Id == titleId);
		}

		public virtual void SortWorks(IEnumerable<Work> sortedWorks)
		{
			if(sortedWorks.Count() != this.SubWorks.Count)
			{
				throw new Exception("sortedWorks count does not match current count of SubWorks");
			}
			for(int i = 0; i< sortedWorks.Count(); i++)
			{
				Work work = sortedWorks.ElementAt(i);
				work.SetViewOrder(i);
			}
		}

		public virtual void SetImageFile(ImageFileInfo imageFileInfo)
		{
			if (imageFileInfo == null)
			{
				throw new DomainLogicException("Image File cannot be null");
			}
			this.File = imageFileInfo;
		}

		public virtual void RemoveImageFile()
		{
			this.File = null;
		}

		public override void ChangeReleaseDate(DateTime releaseDate)
		{
			//TODO: do we want to enforce the release date being the same in collection works that are part of a collection?
			base.ChangeReleaseDate(releaseDate);
			foreach (Work subWork in this.SubWorks)
			{
				subWork.ReleaseDate = releaseDate;
			}
		}

		public override IList<DownloadEvent> ListDownloadEvents()
		{
			List<DownloadEvent> list = new List<DownloadEvent>(base.ListDownloadEvents());
			foreach ( Work work in this.SubWorks )
			{
				list.AddRange(work.ListDownloadEvents());
			}
			return list.AsReadOnly();
		}

		public override IList<PlayEvent> ListPlayEvents()
		{
			List<PlayEvent> list = new List<PlayEvent>(base.ListPlayEvents());
			foreach ( Work work in this.SubWorks )
			{
				list.AddRange(work.ListPlayEvents());
			}
			return list.AsReadOnly();
		}

		#endregion
	}
}
