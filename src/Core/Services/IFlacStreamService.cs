using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MusicCompany.Core.Services
{
	public interface IFlacStreamService
	{
		void WaveToFlac(Stream inputStream, Stream outputStream);
		void FlacToWave(Stream inputStream, Stream outputStream);
	}

	public class FlacStreamServiceStub : IFlacStreamService
	{

		public void WaveToFlac(Stream inputStream, Stream outputStream)
		{
			
		}

		public void FlacToWave(Stream inputStream, Stream outputStream)
		{
			
		}

	}
}
