using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MusicCompany.Core.Services
{
	public interface IMP3StreamService
	{
		/// <summary>
		/// //Encodes a wave stream to mp3
		/// </summary>
		void WaveToMP3(Stream inputStream, Stream outputStream);
		
		/// <summary>
		/// Decodes an mp3 to wav
		/// 
		/// (We won't support this yet...
		/// </summary>
		//void MP3ToWave(Stream inputStream, Stream outputStream);

		/// <summary>
		/// Gets the duration in seconds.
		/// </summary>
		int GetDuration(Stream inputStream);
	}

	public class MP3StreamServiceStub : IMP3StreamService
	{

		public void WaveToMP3(Stream inputStream, Stream outputStream)
		{

		}

		public int GetDuration(Stream inputStream)
		{
			return 0;
		}
	}
}
