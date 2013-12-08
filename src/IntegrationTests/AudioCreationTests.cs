using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using MusicCompany.Core.Factories;
using MusicCompany.Infrastructure;
using MusicCompany.Core;
using MusicCompany.Core.Services.AudioConversion;
using System.IO;

namespace MusicCompany.IntegrationTests
{
	[TestFixture]
	public class AudioCreationTests
	{
		private string audioFolder = "..\\AudioFiles";

		[Test]
		public void CanCreateAudioFromMP3Stream()
		{

			string fileName = "pluck-44khz-stereo-copy.mp3";

			string sourceFilePath = Path.Combine(audioFolder, fileName);

			AudioFileInfo result = null;
			AudioFileInfoFactory factory = new AudioFileInfoFactory();
			factory.FlacStreamService = new FlacBoxFlacStreamService();
			factory.MP3StreamService = new LameMP3StreamService();

			using ( var inputStream = File.OpenRead(sourceFilePath) )
			{
				result = factory.CreateAudio(fileName, FileFormat.MP3, inputStream);
			}
			

			Assert.IsNotNull(result, "Resulting AudioFileInfo was null");
			

			Assert.AreEqual(fileName, result.OriginalFileName, "Filename not equal");
			Assert.IsNotNull(result.BinaryFileData, "BinaryFileData was null");
			Assert.AreEqual(FileFormat.MP3, result.FileFormat,  "resultFile.FileFormat was not MP3");
			Assert.AreNotEqual(0, result.BinaryFileData.Data.Length, "BinaryFileData.Length was 0");

			using (FileStream fileStream = new FileStream(Path.Combine(audioFolder, "result_CanCreateAudioFromMP3Stream.mp3"), FileMode.Create))
			{
				fileStream.Write(result.BinaryFileData.Data, 0, (int)result.ByteCount);
			}
			
		}

		[Test]
		public void CanCreateAudioFromWavStream()
		{

			string fileName = "pluck-44khz-stereo.wav";

			string sourceFilePath = Path.Combine(audioFolder, fileName);

			AudioFileInfo result = null;
			AudioFileInfoFactory factory = new AudioFileInfoFactory();
			factory.FlacStreamService = new FlacBoxFlacStreamService();
			factory.MP3StreamService = new LameMP3StreamService();
			factory.WaveStreamService = new WaveStreamService();

			using ( var inputStream = File.OpenRead(sourceFilePath) )
			{
				result = factory.CreateAudio(fileName, FileFormat.Wave, inputStream);
			}

			Assert.IsNotNull(result, "Resulting AudioFileInfo was null");
			
			Assert.AreEqual(fileName, result.OriginalFileName, "result Filename not equal");
			Assert.IsNotNull(result.BinaryFileData, "result BinaryFileData was null");
			Assert.AreEqual( FileFormat.MP3, result.FileFormat,"result.FileFormat was not MP3");
			Assert.AreNotEqual(0, result.BinaryFileData.Data.Length, "result.BinaryFileData.Length was 0");

			Assert.IsNotNull(result.AlternateFile, "result AlternateFile was null");
			Assert.AreEqual( FileFormat.Flac, result.AlternateFile.FileFormat,"result.AlternateFile.FileFormat was not FLAC");
			Assert.AreNotEqual(0, result.AlternateFile.BinaryFileData.Data.Length, "result.AlternateFile.BinaryFileData.Length was 0");

			using (FileStream fileStream = new FileStream(Path.Combine(audioFolder, "result_CanCreateAudioFromWavStream.mp3"), FileMode.Create))
			{
				fileStream.Write(result.BinaryFileData.Data, 0, (int)result.ByteCount);
			}

			using (FileStream fileStream = new FileStream(Path.Combine(audioFolder, "result_CanCreateAudioFromWavStream.flac"), FileMode.Create))
			{
				fileStream.Write(result.AlternateFile.BinaryFileData.Data, 0, (int)result.ByteCount);
			}

		}

		[Test]
		public void CanCreateAudioFromFlacStream()
		{

			string fileName = "pluck-44khz-stereo-copy.flac";

			string sourceFilePath = Path.Combine(audioFolder, fileName);

			AudioFileInfo result = null;
			AudioFileInfoFactory factory = new AudioFileInfoFactory();
			factory.FlacStreamService = new FlacBoxFlacStreamService();
			factory.MP3StreamService = new LameMP3StreamService();
			factory.WaveStreamService = new WaveStreamService();

			using ( var inputStream = File.OpenRead(sourceFilePath) )
			{
				result = factory.CreateAudio(fileName, FileFormat.Flac, inputStream);
			}

			Assert.IsNotNull(result, "Resulting AudioFileInfo was null");
			
			Assert.AreEqual(fileName, result.OriginalFileName, "result Filename not equal");
			Assert.IsNotNull(result.BinaryFileData, "result BinaryFileData was null");
			Assert.AreEqual( FileFormat.MP3, result.FileFormat,"result.FileFormat was not MP3");
			Assert.AreNotEqual(0, result.BinaryFileData.Data.Length, "result.BinaryFileData.Length was 0");

			Assert.IsNotNull(result.AlternateFile, "result AlternateFile was null");
			Assert.AreEqual( FileFormat.Flac, result.AlternateFile.FileFormat,"result.AlternateFile.FileFormat was not FLAC");
			Assert.AreNotEqual(0, result.AlternateFile.BinaryFileData.Data.Length, "result.AlternateFile.BinaryFileData.Length was 0");

			using (FileStream fileStream = new FileStream(Path.Combine(audioFolder, "result_CanCreateAudioFromFlacStream.mp3"), FileMode.Create))
			{
				fileStream.Write(result.BinaryFileData.Data, 0, (int)result.ByteCount);
			}

			using (FileStream fileStream = new FileStream(Path.Combine(audioFolder, "result_CanCreateAudioFromFlacStream.flac"), FileMode.Create))
			{
				fileStream.Write(result.AlternateFile.BinaryFileData.Data, 0, (int)result.ByteCount);
			}

		}
	}
}
