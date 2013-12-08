using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;

namespace MusicCompany.Core.Services.AudioConversion.Tests
{
	[TestFixture]
	public class LameMP3StreamServiceTester
	{
		private string audioFolder = "..\\AudioFiles";

		[Test]
		public void WavToMP3_File()
		{
			string fileName = "pluck-44khz-stereo";

			string sourceFilePath = Path.Combine(audioFolder, fileName) + ".wav";
			string targetFilePath = Path.Combine(audioFolder, "result_WavToMP3_File") + ".mp3";

			var service = new LameMP3StreamService();

			using (var inputStream = File.OpenRead(sourceFilePath))
			using (var targetStream = File.Create(targetFilePath))
			{
				service.WaveToMP3(inputStream, targetStream);
			}
			
			Assert.IsTrue(File.Exists(targetFilePath), targetFilePath + " not created");
			FileInfo targetInfo = new FileInfo(targetFilePath);
			Assert.AreNotEqual(0, targetInfo.Length, "targetFile is 0 bytes");
			FileInfo sourceInfo = new FileInfo(sourceFilePath);
			Assert.Less(targetInfo.Length, sourceInfo.Length, "target file is not smaller than source");
		}

		//[Test]
		// WaveToMP3 closes the stream when writing to MemoryStream...
		public void WavToMP3_ByteArray()
		{
			string fileName = "pluck-44khz-stereo";
			string sourceFilePath = Path.Combine(audioFolder, fileName) + ".wav";
			var service = new LameMP3StreamService();
			
			byte[] sourceBytes = new byte[0];
			using (var inputStream = File.OpenRead(sourceFilePath))
			{
				sourceBytes = Util.GetBytes(inputStream);
			}
			
			byte[] targetBytes = new byte[0];
			
			using (var inputStream = File.OpenRead(sourceFilePath))
			using (var targetStream = new MemoryStream())
			{
				service.WaveToMP3(inputStream, targetStream);
				targetBytes = Util.GetBytes(targetStream);
			}

			Assert.AreNotEqual(0, targetBytes.Length, "target bytes was 0 length");
			Assert.Less(targetBytes.Length, sourceBytes.Length, "target bytes not less than source bytes");

			using (FileStream fileStream = new FileStream(Path.Combine(audioFolder, "result_WavToMP3_ByteArray.mp3"), FileMode.Create))
			{
				fileStream.Write(targetBytes, 0, targetBytes.Length);
			}
		}


		[Test]
		public void CanDetermineDuration()
		{
			string fileName = "pluck-44khz-stereo-copy";

			string sourceFilePath = Path.Combine(audioFolder, fileName) + ".mp3";

			var service = new LameMP3StreamService();

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
