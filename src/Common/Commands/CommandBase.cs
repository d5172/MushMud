using Agatha.Common;

namespace MusicCompany.Common.Commands
{
	public abstract class CommandRequestBase : Request
	{
		public CommandRequestBase()
		{
			this.CommandContext = CommandContext.Anonymous;
		}

		public CommandContext CommandContext
		{
			get;
			set;
		}
	}


	public abstract class CommandResponseBase : Response
	{
	}
}
