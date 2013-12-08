using System;

namespace MusicCompany.Core
{
	public class DomainLogicException : Exception
	{
		public DomainLogicException(string message) : base(message)
		{
		}
	}
}
