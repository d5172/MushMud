using Agatha.Common;
using MusicCompany.Common.Commands;
using MusicCompany.Core;
using MusicCompany.ServiceLayer.Base;

namespace MusicCompany.ServiceLayer.CommandHandlers
{
	public class CreatePersonCommandHandler : TransactionalRequestHandler<CreatePersonCommand, CreatePersonCommandResponse>
	{
		public override Response Handle(CreatePersonCommand request)
		{
			Person person = new Person(request.Username, request.Name);
			this.Session.Save(person);
			if ( !string.IsNullOrEmpty(request.ArtistName) )
			{
				Artist artist = new Artist(request.ArtistName, person);
				this.Session.Save(artist);
			}
			var response =  this.CreateTypedResponse();
			response.PersonName = person.Name;
			return response;
		}
	}
}