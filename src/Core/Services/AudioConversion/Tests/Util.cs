using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MusicCompany.Core.Services.AudioConversion.Tests
{
	public static class Util
	{

		public static byte[] GetBytes(Stream inputStream)
		{
			byte[] fileBytes = new byte[inputStream.Length];
			inputStream.Read(fileBytes, 0, (int)inputStream.Length);
			return fileBytes;
		}
	}
}
