using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicCompany.Core
{
	/// <summary>
	/// Represents the Entity that is the principal author of works.
	/// Artist exposes the primary API for managing works, to enforce
	/// domain rules such as:
	/// 1) An artist cannot have two works with identical names in the same collection
	/// 2) an artist cannot have two single-works (not in a collection) with the same name
	/// 3) when moving a work into and out of a collection, the vieworders in the collection must be kept sequential
	/// 4) when moving a work into and out of a collection, rule 1 must mot be violated
	/// 
	/// </summary>
	public class Artist : VersionedEntity
	{
		#region Constructors

		protected Artist()
			: base()
		{
			this.ArtistPersons = new List<ArtistPerson>();
			this.SingleWorks = new List<Work>();
			this.CollectionWorks = new List<Work>();
			this.Bio = string.Empty;
		}

		public Artist(string name, Person owningPerson)
			: this()
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new DomainLogicException("Artist name cannot be blank");
			}
			if (owningPerson == null)
			{
				throw new ArgumentNullException("owningPerson");
			}
			this.Name = new UniqueName(name);
			ArtistPerson artistPerson = new ArtistPerson(this, owningPerson);
			this.ArtistPersons.Add(artistPerson);
		}

		#endregion

		#region Properties

		public virtual UniqueName Name
		{
			get;
			protected set;
		}

		public virtual string Bio
		{
			get;
			set;
		}

		public virtual ImageFileInfo ProfilePicture
		{
			get;
			protected set;
		}

		protected virtual IList<ArtistPerson> ArtistPersons
		{
			get;
			set;
		}

		protected virtual IList<Work> SingleWorks
		{
			get;
			set;
		}

		protected virtual IList<Work> CollectionWorks
		{
			get;
			set;
		}

		#endregion

		#region Public Methods

		public virtual bool IsManagedBy(Person person)
		{
			if (person == null)
			{
				throw new ArgumentNullException("person");
			}
			foreach (ArtistPerson manager in this.ArtistPersons)
			{
				if (manager.Person == person)
				{
					return true;
				}
			}
			return false;
		}

		public virtual IEnumerable<ArtistPerson> EnumerateArtistPersons()
		{
			return this.ArtistPersons;
		}

		public virtual IEnumerable<Work> GetCollectionWorks()
		{
			return this.CollectionWorks.OrderBy(w => w.ViewOrder);
		}

		public virtual IEnumerable<Work> GetSingleWorks()
		{
			return this.SingleWorks.OrderBy(w => w.ViewOrder);
		}

		public virtual IEnumerable<Work> GetAllWorks()
		{
			var allWorks = new List<Work>();
			foreach (var collection in this.GetCollectionWorks())
			{
				allWorks.Add(collection);
				allWorks.AddRange(((CollectionWork)collection).GetWorks());
			}
			allWorks.AddRange(this.GetSingleWorks());
			return allWorks;
		}

		public virtual void AddSingleWork(Work work)
		{
			if (work == null)
			{
				throw new ArgumentNullException("work");
			}
			work.CorrectViewOrder(this.SingleWorks);
			work.CorrectTitle(this.SingleWorks);
		    this.SingleWorks.Add(work);
		}

		public virtual void RemoveSingleWork(Work work)
		{
			if (work == null)
			{
				throw new ArgumentNullException("work");
			}
			work.Leave(this.SingleWorks);
			this.SingleWorks.Remove(work);
		}

		public virtual void AddCollectionWork(CollectionWork work)
		{
			if (work == null)
			{
				throw new ArgumentNullException("work");
			}
			work.CorrectViewOrder(this.CollectionWorks);
			work.CorrectTitle(this.CollectionWorks);
		    this.CollectionWorks.Add(work);
		}

		public virtual void RemoveCollectionWork(CollectionWork work)
		{
			if (work == null)
			{
				throw new ArgumentNullException("work");
			}
			work.Leave(this.CollectionWorks);
			this.CollectionWorks.Remove(work);
		}

		public virtual void AddWorkToParentWork(Work work, CollectionWork parentWork)
		{
			if (work == null)
			{
				throw new ArgumentNullException("work");
			}
			if (parentWork == null)
			{
				throw new ArgumentNullException("parentWork");
			}

			// even though this keeps the model more correct after the operation,
			// it requires that we set cascade="save-update" and explicitly delete
			// single works.  We want the removal of single works to come through
			// the artist (or parent work) so the view orders can be corrected.
			//if (this.SingleWorks.Contains(work))
			//{
			//    this.RemoveSingleWork(work);
			//}

			parentWork.AddWork(work);
		}

		public virtual void RemoveWorkFromParent(Work work)
		{
			if (work == null)
			{
				throw new ArgumentNullException("work");
			}
			work.ParentWork.RemoveWork(work);
			this.AddSingleWork(work);
		}

		public virtual Work GetSingleWork(string titleId)
		{
			if (string.IsNullOrEmpty(titleId))
			{
				throw new ArgumentNullException("titleId");
			}
			return this.SingleWorks.Single(w => w.Title.Id == titleId);
		}

		public virtual CollectionWork GetCollectionWork(string titleId)
		{
			if (string.IsNullOrEmpty(titleId))
			{
				throw new ArgumentNullException("titleId");
			}
			return this.CollectionWorks.Single(w => w.Title.Id == titleId) as CollectionWork;
		}

		public virtual void ChangeWorkTitle(Work work, string newTitle)
		{
			if (work == null)
			{
				throw new ArgumentNullException("work");
			}
			if (string.IsNullOrEmpty(newTitle))
			{
				throw new DomainLogicException("New Title cannot be blank");
			}
			if (work.ParentWork != null)
			{
				work.ChangeTitle(newTitle, work.ParentWork.GetWorks());
			}
			else
			{
				work.ChangeTitle(newTitle, this.GetSingleWorks());
			}
		}

		public virtual void ChangeCollectionTitle(CollectionWork work, string newTitle)
		{
			if (work == null)
			{
				throw new ArgumentNullException("work");
			}
			if (string.IsNullOrEmpty(newTitle))
			{
				throw new DomainLogicException("New Title cannot be blank");
			}
			if (work.ParentWork != null)
			{
				work.ChangeTitle(newTitle, work.ParentWork.GetWorks());
			}
			else
			{
				work.ChangeTitle(newTitle, this.GetCollectionWorks());
			}
		}

		public virtual void ChangeName(string newName)
		{
			if (string.IsNullOrEmpty(newName))
			{
				throw new DomainLogicException("New name cannot be blank");
			}
			this.Name.Change(newName);
		}

		public virtual void SetProfilePicture(ImageFileInfo imageFileInfo)
		{
			if (imageFileInfo == null)
			{
				throw new DomainLogicException("Profile picture cannot be null");
			}
			this.ProfilePicture = imageFileInfo;
		}

		public virtual void RemoveProfilePicture()
		{
			this.ProfilePicture = null;
		}

		#endregion
	}
}
