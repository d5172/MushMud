using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;

namespace MusicCompany.Core.Services.AudioConversion.Tests
{
	[TestFixture]
	public class WaveStreamServiceTester
	{
		private string audioFolder = "..\\AudioFiles";

		[Test]
		public void CanDetermineDuration()
		{
		
			string fileName = "pluck-44khz-stereo";

			string sourceFilePath = Path.Combine(audioFolder, fileName) + ".wav";

			var service = new WaveStreamService();

			int duration = 0;
			using (var inputStream = File.OpenRead(sourceFilePath))
			{
				duration = service.GetDuration(inputStream);
			}

			Assert.AreNotEqual(0, duration, "Duration was 0");
			Console.WriteLine(TimeSpan.FromSeconds(duration));
		
		}
	}
}
