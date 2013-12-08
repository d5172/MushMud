using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using System.IO;

namespace MusicCompany.Core.Services.AudioConversion.Tests
{
	[TestFixture]
	public class FlacBoxFlacStreamServiceTester
	{

		private string audioFolder = "..\\AudioFiles";
		

		[Test]
		public void WavToFlac_File()
		{
			string fileName = "pluck-44khz-stereo";

			string sourceFilePath = Path.Combine(audioFolder, fileName) + ".wav";
			string targetFilePath = Path.Combine(audioFolder, "result_WavToFlac_File") + ".flac";

			var service = new FlacBoxFlacStreamService();

			using (var inputStream = File.OpenRead(sourceFilePath))
			using (var targetStream = File.Create(targetFilePath))
			{
				service.WaveToFlac(inputStream, targetStream);
			}
			
			Assert.IsTrue(File.Exists(targetFilePath), targetFilePath + " not created");
			FileInfo targetInfo = new FileInfo(targetFilePath);
			Assert.AreNotEqual(0, targetInfo.Length, "targetFile is 0 bytes");
			FileInfo sourceInfo = new FileInfo(sourceFilePath);
			Assert.Less(targetInfo.Length, sourceInfo.Length, "target file is not smaller than source");

			
		}


		[Test]
		public void WavToFlac_ByteArray()
		{
			string fileName = "pluck-44khz-stereo";
			string sourceFilePath = Path.Combine(audioFolder, fileName) + ".wav";
			var service = new FlacBoxFlacStreamService();
			
			byte[] sourceBytes = new byte[0];
			using (var inputStream = File.OpenRead(sourceFilePath))
			{
				sourceBytes = Util.GetBytes(inputStream);
			}
			
			byte[] targetBytes = new byte[0];
			
			using (var inputStream = File.OpenRead(sourceFilePath))
			using (var targetStream = new MemoryStream())
			{
				service.WaveToFlac(inputStream, targetStream);
				targetBytes = Util.GetBytes(targetStream);
			}

			Assert.AreNotEqual(0, targetBytes.Length, "target bytes was 0 length");
			Assert.Less(targetBytes.Length, sourceBytes.Length, "target bytes not less than source bytes");

			using (FileStream fileStream = new FileStream(Path.Combine(audioFolder, "result_WavToFlac_ByteArray.flac"), FileMode.Create))
			{
				fileStream.Write(targetBytes, 0, targetBytes.Length);
			}
		}

		[Test]
		public void FlacToWav_File()
		{
			string fileName = "pluck-44khz-stereo-copy";

			string sourceFilePath = Path.Combine(audioFolder, fileName) + ".flac";
			string targetFilePath = Path.Combine(audioFolder, "result_FlacToWav_File") + ".wav";

			var service = new FlacBoxFlacStreamService();

			using (var inputStream = File.OpenRead(sourceFilePath))
			using (var targetStream = File.Create(targetFilePath))
			{
				service.FlacToWave(inputStream, targetStream);
			}
			
			Assert.IsTrue(File.Exists(targetFilePath), targetFilePath + " not created");
			FileInfo targetInfo = new FileInfo(targetFilePath);
			Assert.AreNotEqual(0, targetInfo.Length, "targetFile is 0 bytes");
			FileInfo sourceInfo = new FileInfo(sourceFilePath);
			Assert.Greater(targetInfo.Length, sourceInfo.Length, "target file is not larger than source");
		}

		[Test]
		public void FlacToWav_ByteArray()
		{
			string fileName = "pluck-44khz-stereo-copy";
			string sourceFilePath = Path.Combine(audioFolder, fileName) + ".flac";
			var service = new FlacBoxFlacStreamService();
			
			byte[] sourceBytes = new byte[0];
			using (var inputStream = File.OpenRead(sourceFilePath))
			{
				sourceBytes = Util.GetBytes(inputStream);
			}
			
			byte[] targetBytes = new byte[0];
			
			using (var inputStream = File.OpenRead(sourceFilePath))
			using (var targetStream = new MemoryStream())
			{
				service.FlacToWave(inputStream, targetStream);
				targetBytes = Util.GetBytes(targetStream);
			}

			Assert.AreNotEqual(0, targetBytes.Length, "target bytes was 0 length");
			Assert.Greater(targetBytes.Length, sourceBytes.Length, "target bytes not greater than source bytes");

			using (FileStream fileStream = new FileStream(Path.Combine(audioFolder, "result_FlacToWav_ByteArray.wav"), FileMode.Create))
			{
				fileStream.Write(targetBytes, 0, targetBytes.Length);
			}
		}
	}
}
