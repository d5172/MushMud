using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using MusicCompany.Core.Services;
using MusicCompany.Infrastructure;

namespace MusicCompany.Core.Factories
{
	#region Interface

	public interface IAudioFileInfoFactory
	{
		/// <summary>
		/// Returns a new AudioFileInfo instance.
		/// </summary>
		AudioFileInfo CreateAudio(string originalFileName, FileFormat inputFormat, Stream inputStream);

	}

	#endregion

	#region Implementation

	public class AudioFileInfoFactory : IAudioFileInfoFactory
	{
		#region Properties

		public IWaveStreamService WaveStreamService
		{
			get;
			set;
		}

		public IFlacStreamService FlacStreamService
		{
			get;
			set;
		}

		public IMP3StreamService MP3StreamService
		{
			get;
			set;
		}

		/// <summary>
		/// Path to directory where temporary files can be written to and deleted
		/// while doing format conversion work.
		/// </summary>
		public IFilePath WorkingDirectory
		{
			get;
			set;
		}

		#endregion

		protected virtual string GetNewTempFileName()
		{
			return Path.Combine(this.WorkingDirectory.GetPath(), Guid.NewGuid().ToString());
		}

		public AudioFileInfo CreateAudio(string fileName, FileFormat inputFormat, Stream inputStream)
		{
			if (!inputFormat.IsAudioType())
			{
				throw new InvalidOperationException(string.Format("{0} is not a supported audio format.", inputFormat));
			}

			int seconds = 0;
			byte[] primaryByteArray = new byte[0];
			byte[] alternateByteArray = new byte[0];
			
			switch (inputFormat)
			{
				case FileFormat.Wave:
					
					//capture wav bytes
					byte[] wavBytes = inputStream.GetBytes();
					
					//get duration
					using (var waveStream = new MemoryStream(wavBytes))
					{
						seconds = this.WaveStreamService.GetDuration(waveStream);
					}

					//write out a temp flac file
					string tempFlacFileName = this.GetNewTempFileName();
					using (var waveStream = new MemoryStream(wavBytes))
					using (var flacStream = File.Create(tempFlacFileName))
					{
						this.FlacStreamService.WaveToFlac(waveStream, flacStream);
					}

					//read in flac file into bytes
					using (var outputStream = File.OpenRead(tempFlacFileName))
					{
						alternateByteArray = outputStream.GetBytes();
					}

					//write out a temp mp3 file
					string tempMP3FileName = this.GetNewTempFileName();
					using (var waveStream = new MemoryStream(wavBytes))
					using (var mp3Stream = File.Create(tempMP3FileName))
					{
						this.MP3StreamService.WaveToMP3(waveStream, mp3Stream);
					}

					//read in mp3 file into bytes
					using (var outputStream = File.OpenRead(tempMP3FileName))
					{
						primaryByteArray = outputStream.GetBytes();
					}

					//cleanup
					File.Delete(tempFlacFileName);
					File.Delete(tempMP3FileName);

					break;
				case FileFormat.Flac:
					
					//capture the flac bytes
					alternateByteArray = inputStream.GetBytes();

					//create a temp wav file
					string tempWavFileName = this.GetNewTempFileName();
					using (var newStream = new MemoryStream(alternateByteArray))
					{
						using (var createStream = File.Create(tempWavFileName))
						{
							this.FlacStreamService.FlacToWave(newStream, createStream);
						}
					}

					//write out a temp mp3 file
					string tempMP3File = this.GetNewTempFileName();
					using (var waveStream = File.OpenRead(tempWavFileName))
					using (var mp3Stream = File.Create(tempMP3File))
					{
						this.MP3StreamService.WaveToMP3(waveStream, mp3Stream);
					}

					//read in mp3 file into bytes
					using (var outputStream = File.OpenRead(tempMP3File))
					{
						primaryByteArray = outputStream.GetBytes();
					}

					//get the duration
					using (var waveStream = File.OpenRead(tempWavFileName))
					{
						seconds = this.WaveStreamService.GetDuration(waveStream);
					}

					//cleanup
					File.Delete(tempWavFileName);
					File.Delete(tempMP3File);

					break;
				case FileFormat.MP3:
					
					//capture mp3 bytes
					primaryByteArray = inputStream.GetBytes();

					//create a new stream to get duration
					using (var tempStream = new MemoryStream(primaryByteArray))
					{
						seconds = this.MP3StreamService.GetDuration(tempStream);
					}
					
					break;
				default:
					break;
			}

			BinaryFileData fileData = new BinaryFileData(primaryByteArray);
			AudioFileInfo audioFile = new AudioFileInfo(Path.GetFileName(fileName), FileFormat.MP3, primaryByteArray.Length, fileData, seconds);

			if (alternateByteArray.Length > 0)
			{
				BinaryFileData alternateFileData = new BinaryFileData(alternateByteArray);
				AudioFileInfo alternateFile = new AudioFileInfo(Path.GetFileName(fileName), FileFormat.Flac, alternateByteArray.Length, alternateFileData, seconds);
				audioFile.SetAlternateFile(alternateFile);
			}
			return audioFile;
		}

	#endregion
	}
}
