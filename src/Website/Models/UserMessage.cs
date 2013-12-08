using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MusicCompany.Website.Models
{
	public class UserMessage
	{
		public UserMessage()
		{
		}

		public UserMessage(string message, UserMessageType type)
		{
			this.Message = message;
			this.Type = type;
		}

		public string Message
		{
			get;
			set;
		}

		public UserMessageType Type
		{
			get;
			set;
		}
	}

	public enum UserMessageType
	{
		Error,
		Info,
		Warning
	}
}
