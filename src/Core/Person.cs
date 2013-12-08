using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicCompany.Core
{
	public class Person : VersionedEntity
	{
		protected Person()
		{
		}

		public Person(string username, string name)
		{
			if (string.IsNullOrEmpty(username))
			{
				throw new DomainLogicException("username cannot be blank");
			}
			if (string.IsNullOrEmpty(name))
			{
				throw new DomainLogicException("Name cannot be blank");
			}
			this.Username = username;
			this.Name = name;
		}

		public virtual string Username
		{
			get;
			protected set;
		}

		public virtual string Name
		{
			get;
			protected set;
		}

		public virtual ImageFileInfo ProfilePicture
		{
			get;
			protected set;
		}
	}
}
