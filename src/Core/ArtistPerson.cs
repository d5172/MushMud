
using System;
namespace MusicCompany.Core
{
	/// <summary>
	/// Represents a Person with a role in managing an Artist
	/// </summary>
	public class ArtistPerson : VersionedEntity
	{
		#region Constructors

		protected ArtistPerson() : base()
		{
		}

		public ArtistPerson(Artist artist, Person person) : this()
		{
			if (artist == null)
			{
				throw new ArgumentNullException("artist");
			}
			if (person == null)
			{
				throw new ArgumentNullException("person");
			}
			this.Artist = artist;
			this.Person = person;
		}

		#endregion

		#region Properties

		public virtual Artist Artist
		{
			get;
			protected set;
		}

		public virtual Person Person
		{
			get;
			protected set;
		}

		#endregion
	}
}
