using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MusicCompany.Core.Factories
{
	public static class StreamExtensions
	{

		public static byte[] GetBytes(this Stream inputStream)
		{
			byte[] fileBytes = new byte[inputStream.Length];
			inputStream.Read(fileBytes, 0, (int)inputStream.Length);
			inputStream.Close();
			return fileBytes;
		}

		public static Stream CopyAndCloseStream(this Stream inputStream)
		{
			const int readSize = 256;
			byte[] buffer = new byte[readSize];
			MemoryStream ms = new MemoryStream();
			int count = inputStream.Read(buffer, 0, readSize);
			while (count > 0)
			{
				ms.Write(buffer, 0, count);
				count = inputStream.Read(buffer, 0, readSize);
			}
			ms.Seek(0, SeekOrigin.Begin);
			inputStream.Close();
			return ms;
		}

	}
}
