using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MusicCompany.Core.Framework
{
	/// <summary>
	/// Allows applying string value to an enum value
	/// </summary>
	public class StringValueAttribute : Attribute
	{
		public StringValueAttribute(string value)
		{
			this.Value = value;
		}

		public string Value
		{
			get;
			private set;
		}
	}
}
