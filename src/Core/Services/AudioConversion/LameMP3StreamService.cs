using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MusicCompany.Core.Services.AudioConversion.Lame;
using System.IO;

namespace MusicCompany.Core.Services.AudioConversion
{
	public class LameMP3StreamService : IMP3StreamService
	{
		public void WaveToMP3(Stream inputStream, Stream outputStream)
		{
			WaveStream waveStream = new WaveStream(inputStream);
			Mp3WriterConfig config = new Mp3WriterConfig(waveStream.Format);
			using (Mp3Writer writer = new Mp3Writer(outputStream, config))
			{
				byte[] buff = new byte[writer.OptimalBufferSize];
				int read = 0;
				int actual = 0;
				long total = waveStream.Length;
				while ((read = waveStream.Read(buff, 0, buff.Length)) > 0)
				{

					writer.Write(buff, 0, read);
					actual += read;
				}
			}
		}

		public int GetDuration(Stream inputStream)
		{
			MP3Header header = new MP3Header();
			header.ReadMP3Information(inputStream, true);
			return header.intLength;
		}
	}
}
