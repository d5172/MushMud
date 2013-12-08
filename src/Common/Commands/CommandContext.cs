
namespace MusicCompany.Common.Commands
{
	public class CommandContext
	{
		public static readonly CommandContext Anonymous = new CommandContext("");

		public CommandContext(string username)
		{
			this.Username = username;
		}

		public string Username
		{
			get;
			private set;
		}

		public override bool Equals(object obj)
		{
			if ( object.ReferenceEquals(this, obj) )
			{
				return true;
			}
			var other = obj as CommandContext;
			if ( other != null )
			{
				return other.Username.Equals(this.Username);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return this.Username.GetHashCode() * 397;
		}
	}
}
