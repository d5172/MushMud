using System;
using System.Collections.Generic;

namespace MusicCompany.Common.Commands
{
	public class SortChildWorksCommand : CommandRequestBase
	{
		public string ArtistIdentifier
		{
			get;
			set;
		}

		public string CollectionIdentifier
		{
			get;
			set;
		}

		public IList<string> SortedIdentifiers
		{
			get;
			set;
		}
	}

	public class SortChildWorksCommandResponse : CommandResponseBase
	{
		public string CollectionTitle
		{
			get;
			set;
		}
	}
}