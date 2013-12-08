using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace MusicCompany.Core.Services.AudioConversion
{
	public class WaveStreamService : IWaveStreamService
	{
		public int GetDuration(Stream waveStream)
		{
			WaveStream ws = new WaveStream(waveStream);
			if (ws.Format.nAvgBytesPerSec != 0)
			{
				return Convert.ToInt32(Math.Ceiling(Convert.ToDouble(ws.DataLength / ws.Format.nAvgBytesPerSec)));
			}
			return 0;
		}
	}
}
