using System;
using System.Linq;
using MusicCompany.Common.Commands;
using MusicCompany.Core;
using NHibernate.Linq;

namespace MusicCompany.ServiceLayer.Base
{
	public abstract class CommandHandler<TRequest, TResponse> : TransactionalRequestHandler<TRequest, TResponse>
		where TRequest : CommandRequestBase
		where TResponse : CommandResponseBase
	{
		protected virtual Artist GetArtistByIdentifier(string artistIdentifier)
		{
			Artist artist = this.Session.Linq<Artist>().SingleOrDefault(a => a.Name.Id == artistIdentifier);
			if ( artist == null )
			{
				throw new ApplicationException(string.Format("Unknown artist ({0})", artistIdentifier));
			}
			return artist;
		}

		protected virtual Person GetPersonByIdentifier(string userName)
		{
			Person person = this.Session.Linq<Person>().SingleOrDefault(p => p.Username == userName);
			if ( person == null )
			{
				throw new ApplicationException(string.Format("Unknown person ({0})", userName));
			}
			return person;
		}

		protected virtual Person GetPersonFromCommandContext(CommandRequestBase command)
		{
			if ( command.CommandContext.Equals(CommandContext.Anonymous) )
			{
				return null;
			}
			return this.GetPersonByIdentifier(command.CommandContext.Username);
		}

		protected virtual void CheckArtistManagementPermissions(CommandRequestBase command, Artist artist)
		{
			if ( !artist.IsManagedBy(this.GetPersonFromCommandContext(command)) )
			{
				throw new ApplicationException("You are not allowed to manage that artist");
			}
		}
	}
}
