using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using FlacBox;

namespace MusicCompany.Core.Services.AudioConversion
{
	public class FlacBoxFlacStreamService : IFlacStreamService
	{
		
		private static void CopyStreams(Stream s, Stream t)
        {
            byte[] buffer = new byte[0x1000];
            int read = s.Read(buffer, 0, buffer.Length);
            while (read > 0)
            {
                t.Write(buffer, 0, read);
                read = s.Read(buffer, 0, buffer.Length);
            }
        }

		public void WaveToFlac(Stream inputStream, Stream outputStream)
		{
			Stream flacStream = new WaveOverFlacStream(outputStream, WaveOverFlacStreamMode.Encode, true);
			CopyStreams(inputStream, flacStream);
		}

		public void FlacToWave(Stream inputStream, Stream outputStream)
		{
			Stream wavStream = new WaveOverFlacStream(inputStream, WaveOverFlacStreamMode.Decode, true);
			CopyStreams(wavStream, outputStream);
		}
	}
}
