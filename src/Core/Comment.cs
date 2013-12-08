using System;

namespace MusicCompany.Core
{
	public class Comment : Entity
	{
		protected Comment() : base()
		{
		}

		public Comment(Work work, Person person, string commentText)
		{
			if ( work == null )
			{
				throw new ArgumentNullException("work");
			}
			if ( person == null )
			{
				throw new ArgumentNullException("person");
			}
			if ( string.IsNullOrEmpty(commentText) )
			{
				throw new ArgumentNullException("commentText");
			}
			this.Work = work;
			this.Person = person;
			this.CommentText = commentText;
			this.DateEntered = DateTime.Now;
		}

		public virtual Work Work
		{
			get;
			protected set;
		}

		public virtual Person Person
		{
			get;
			protected set;
		}

		public virtual string CommentText
		{
			get;
			protected set;
		}

		public virtual DateTime DateEntered
		{
			get;
			protected set;
		}
		
	}
}