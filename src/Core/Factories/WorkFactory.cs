using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicCompany.Core.Factories
{
	public interface IWorkFactory
	{
		Work CreateAudio(Artist artist, Work parentWork, string title);
		Work CreateCollection(Artist artist, string title);
	}
}
