using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MusicCompany.Core.Services
{
	/// <summary>
	/// manipulation of WAV data.
	/// </summary>
	public interface IWaveStreamService
	{
		/// <summary>
		/// Gets the duration in seconds.
		/// </summary>
		int GetDuration(Stream waveStream);
	}

	public class WaveStreamServiceStub : IWaveStreamService
	{

		public int GetDuration(Stream waveStream)
		{
			return 0;
		}
	}
}
