﻿using System;

namespace MusicCompany.Core
{
	public class Entity : IEquatable<Entity>
	{
		protected Entity()
		{
			
		}
		
		public virtual Guid Id
		{
			get;
			protected set;
		}

		public virtual bool Equals(Entity obj)
		{
			if ( ReferenceEquals(null, obj) )
			{
				return false;
			}
			if ( ReferenceEquals(this, obj) )
			{
				return true;
			}
			if ( GetType() != obj.GetType() )
			{
				return false;
			}
			return obj.Id == Id;
		}

		public override bool Equals(object obj)
		{
			if ( ReferenceEquals(null, obj) )
			{
				return false;
			}
			if ( ReferenceEquals(this, obj) )
			{
				return true;
			}
			if ( GetType() != obj.GetType() )
			{
				return false;
			}
			return Equals((Entity)obj);
		}

		public override int GetHashCode()
		{
			return (Id.GetHashCode() * 397) ^ GetType().GetHashCode();
		}

		public static bool operator ==(Entity left, Entity right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(Entity left, Entity right)
		{
			return !Equals(left, right);
		}
	}
}
