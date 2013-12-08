using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using FluorineFx.Messaging.Adapter;
using FluorineFx.Messaging.Api;
using FluorineFx.Messaging.Api.Stream;
using FluorineFx.Messaging.Api.Stream.Support;
using FluorineFx.Messaging.Rtmp.Stream;
using MusicCompany.Core;
using MusicCompany.Core.Repositories;
using MusicCompany.Infrastructure;
using MusicCompany.Core.Services;

namespace MusicCompany.FlexServices
{
	public class PlayerAdapter : ApplicationAdapter, IStreamAwareScopeHandler
	{

		private string streamFilePath;
		private IDictionary<IConnection, IServerStream> streamDictionary;

		public IBinaryFileInfoRepository BinaryFileInfoRepository
		{
			get;
			set;
		}

		public IMP3StreamService MP3StreamService
		{
			get;
			set;
		}

		public IFlacStreamService FlacStreamService
		{
			get;
			set;
		}

		private void InitializeBinaryFileInfoRepository()
		{
			if (this.BinaryFileInfoRepository == null)
			{
				this.BinaryFileInfoRepository = Container.GetObject<IBinaryFileInfoRepository>();
			}
		}

		private void InitializeMP3StreamService()
		{
			if (this.MP3StreamService == null)
			{
				this.MP3StreamService = Container.GetObject<IMP3StreamService>();
			}
		}

		private void InitializeFlacStreamService()
		{
			if (this.FlacStreamService == null)
			{
				this.FlacStreamService = Container.GetObject<IFlacStreamService>();
			}
		}

		private void SetStreamFilePath()
		{
			this.streamFilePath = HttpContext.Current.Server.MapPath("~/apps/Player/Streams");
		}

		private void EnsureStreamableFileExists(Guid binaryFileInfoId, string filePath)
		{
			if (!File.Exists(filePath))
			{
				BinaryFileInfo binaryFileInfo = this.BinaryFileInfoRepository.Get(binaryFileInfoId);
				if (binaryFileInfo.FileFormat == FileFormat.MP3)
				{
					using (var fileStream = new FileStream(filePath, FileMode.Create))
					{
						fileStream.Write(binaryFileInfo.BinaryFileData.Data, 0, (int)binaryFileInfo.ByteCount);
						fileStream.Close();
					}
				}
				else
				{
					string tempFileName = System.IO.Path.Combine(this.streamFilePath, Guid.NewGuid().ToString() + ".wav");
					using (var flacStream = new MemoryStream(binaryFileInfo.BinaryFileData.Data))
					using (var wavStream = new FileStream(tempFileName, FileMode.Create))
					{
						this.FlacStreamService.FlacToWave(flacStream, wavStream);
						wavStream.Close();
					}
					using (var fileStream = File.OpenRead(tempFileName))
					using (FileStream mp3Stream = new FileStream(filePath, FileMode.Create))
					{
						this.MP3StreamService.WaveToMP3(fileStream, mp3Stream);
						mp3Stream.Close();
					}
					try
					{
						File.Delete(tempFileName);
					}
					catch
					{
					}
				}
			}
		}

		public override bool AppStart(IScope application)
		{
			this.InitializeBinaryFileInfoRepository();
			this.InitializeMP3StreamService();
			this.InitializeFlacStreamService();
			this.SetStreamFilePath();
			this.streamDictionary = new Dictionary<IConnection, IServerStream>();
			return base.AppStart(application);
		}

		/// <summary>
		/// Called when the player client connects
		/// a new netConnection.  Player should supply
		/// the streamId as an argument (which it received via flashVars).
		/// This starts up and publishes a stream to which the client should
		/// subscribe (via a netStream object) to  play the audio.
		/// </summary>
		public override bool AppConnect(IConnection connection, object[] parameters)
		{
			using (UnitOfWork uow = new UnitOfWork())
			{
				//using streamId as BinaryFileInfo.Id - could possibly have player specify both the fileId and a unique streamId if necessary
				string streamId = parameters[0].ToString();

				string targetFileName = streamId + ".mp3";
				string filePath = System.IO.Path.Combine(this.streamFilePath, targetFileName);

				this.EnsureStreamableFileExists(new Guid(streamId), filePath);
				
				FileInfo fileInfo = new FileInfo(filePath);
				long fileSize = fileInfo.Length;

				//start a new server stream
				IServerStream stream = StreamUtils.CreateServerStream(connection.Scope, streamId);
				SimplePlayItem item = new SimplePlayItem();
				item.Name = targetFileName;
				item.Length = fileSize;
				stream.AddItem(item);
				stream.Start();

				//keep track of stream in a dictionary, keyed by the connection
				this.streamDictionary.Add(connection, stream);
			}

			return base.AppConnect(connection, parameters);
		}

		public override void AppDisconnect(IConnection connection)
		{
			base.AppDisconnect(connection);

			//close the stream and remove from dictionary
			if (this.streamDictionary.ContainsKey(connection))
			{
				this.streamDictionary[connection].Close();
				this.streamDictionary.Remove(connection);
			}

		}

		public void StreamSubscriberStart(ISubscriberStream stream)
		{
			//seems to have no effect
			//stream.SetClientBufferDuration(20);
		}

		public void StreamSubscriberClose(ISubscriberStream stream)
		{

		}

		public void StreamBroadcastClose(IBroadcastStream stream)
		{

		}

		public void StreamBroadcastStart(IBroadcastStream stream)
		{

		}

		public void StreamPublishStart(IBroadcastStream stream)
		{

		}

		public void StreamRecordStart(IBroadcastStream stream)
		{

		}

		public void StreamPlaylistItemPlay(IPlaylistSubscriberStream stream, IPlayItem item, bool isLive)
		{

		}

		public void StreamPlaylistItemStop(IPlaylistSubscriberStream stream, IPlayItem item)
		{

		}

		public void StreamPlaylistVODItemPause(IPlaylistSubscriberStream stream, IPlayItem item, int position)
		{
			//this.streams[stream.Connection].Pause();
		}

		public void StreamPlaylistVODItemResume(IPlaylistSubscriberStream stream, IPlayItem item, int position)
		{
			//this.streams[stream.Connection].Seek(position);
			//this.streams[stream.Connection].Pause();
		}

		public void StreamPlaylistVODItemSeek(IPlaylistSubscriberStream stream, IPlayItem item, int position)
		{

		}

	}
}
