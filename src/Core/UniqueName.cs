using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace MusicCompany.Core
{
	public class UniqueName
	{

		protected UniqueName()
		{
		}

		public UniqueName(string name)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new DomainLogicException("Name cannot be blank");
			}
			this.Value = name.Trim();
			this.SetId();
		}

		public virtual string Value
		{
			get;
			protected set;
		}

		public virtual string Id
		{
			get;
			protected set;
		}

		protected internal virtual void Change(string newName)
		{
			this.Value = newName.Trim();
			this.SetId();
		}

		protected void SetId()
		{
			string cleaned = this.Value.Replace(" ", "-");
			cleaned = cleaned.Replace(".", "-");
			this.Id = Regex.Replace(cleaned, @"[^\w\.@-]", "");
		}

		public override string ToString()
		{
			return this.Value;
		}

		public virtual string ToSafePathName()
		{
			string fileName = this.Value;
			foreach ( char c in Path.GetInvalidFileNameChars() )
			{
				fileName = fileName.Replace(c.ToString(), "-");
			}
			foreach ( char c in Path.GetInvalidPathChars() )
			{
				fileName = fileName.Replace(c.ToString(), "-");
			}
			return fileName;
		}
	}
}
