package controllers 
{
	import flash.net.NetConnection;
	import flash.net.NetStream;
	import flash.utils.Timer;
	import mx.binding.utils.ChangeWatcher;
	import mx.containers.*;
	import mx.controls.Button;
	import mx.controls.Label;
	import mx.controls.ProgressBar;
	import mx.controls.TextArea;
	import mx.core.Application;
	import mx.binding.utils.BindingUtils;
	import mx.core.Container;
	import mx.events.*;
	import flash.events.*;
	import mx.controls.Alert;
	import mx.messaging.Channel;
	import mx.messaging.config.ServerConfig;
	
	/**
	 * ...
	 * @author David Martines
	 */
	public class Main extends Application
	{
		public var boxContainer:Container;
		public var btnPlay:Button;
		public var btnClose:Button;
		public var boxPlaying:VBox;
		public var txtLog:TextArea;
		public var lblTitle:Label;
		public var lblTime:Label;
		public var lblLoading:Label;
		public var progressPlay:ProgressBar;
		
		private var netConnection:NetConnection = null;
		private var netStream:NetStream = null;
		
		private var streamId:String;
		private var workDuration:int;
		private var durationString:String;
		
		private var isPlaying:Boolean = false;
		private var isPaused:Boolean = false;
		private var isBuffering:Boolean = false;
		
		private var timeUpdater:Timer = null;
		
		private var bufferTime:int = 3;

		public function Main() 
		{
			this.addEventListener(FlexEvent.CREATION_COMPLETE, creationCompleteHandler);
		}
		
		private function creationCompleteHandler(e:FlexEvent):void 
		{
			boxPlaying.visible = false;
			
			if (this.parameters.enableLog != null) 
			{
				var enableLog:Boolean = Boolean(this.parameters.enableLog);
				if (enableLog)
				{
					this.txtLog = new TextArea();
					this.txtLog.width = 300;
					this.txtLog.height = 300;
					this.boxContainer.addChild(this.txtLog);			
				}
			}
			
			this.streamId = this.parameters.streamId;
			if (this.streamId == null || this.streamId == "")
			{
				log("streamId not specified");
				this.enabled = false;
				return;
			}
			
			var title:String = this.parameters.workTitle;
			if (title == null || title == "")
			{
				log("workTitle not specified");
				return;
			}
			this.lblTitle.text = title;
			
			this.workDuration = int(this.parameters.workDuration);
			if (this.workDuration == 0)
			{
				log("workDuration not specified");
				return;
			}
			this.resetProgress();			
			this.durationString = this.getTimeString(this.workDuration);
			this.displayTime();
			
			btnPlay.addEventListener(MouseEvent.CLICK, btnPlayClickHandler);
			btnClose.addEventListener(MouseEvent.CLICK, btnCloseClickHandler);
		}
		
		private function btnPlayClickHandler(e:MouseEvent):void 
		{
			if (!boxPlaying.visible)
			{
				boxPlaying.visible =  true;
			}
			
			/* pause not supported yet (not pausing stream  on server)*/
			if (this.isPaused)
			{
				this.resume();
				this.btnPlay.label = ">";
				this.btnPlay.toolTip = "Play";
			}
			else if (this.isPlaying) 
			{
				this.pause();
				this.btnPlay.label = ">";
				this.btnPlay.toolTip = "Play";
			}
			else
			{
				this.connect();
				this.btnPlay.label = "||";
				this.btnPlay.toolTip = "Pause";
			}
			
			
			/*
			if (this.isPlaying)
			{
				this.stop();
				this.disconnect();
				this.btnPlay.label = ">";
				this.btnPlay.toolTip = "Play";
			}
			else
			{
				this.connect();
				this.btnPlay.label = "*";
				this.btnPlay.toolTip = "Stop";
			}
			*/
		}
		
		private function btnCloseClickHandler(e:MouseEvent):void 
		{
			this.stop();
			this.disconnect();
			this.resetDisplay();
		}
		
		
		private function resetDisplay():void
		{
			this.btnPlay.label = ">";
			this.btnPlay.toolTip = "Play";
			this.resetProgress();
			boxPlaying.visible = false;
		}
		
		private function displayTime():void 
		{
			if (this.netStream == null)
			{
				this.lblTime.text = "00:00/" + this.durationString;
			}
			else 
			{
				this.lblTime.text = this.getTimeString(this.netStream.time) + "/" + this.durationString;
			}
		}
		
		private function displayProgress():void 
		{
			if (this.netStream != null)
			{
				if (this.isBuffering)
				{
					this.progressPlay.setProgress(this.netStream.bufferLength, this.netStream.bufferTime);
				}
				else
				{
					this.progressPlay.setProgress(this.netStream.time, this.workDuration);
				}
			}
			else 
			{
				this.resetProgress();
			}
		}
		
		private function resetProgress():void 
		{
			this.progressPlay.maximum = this.workDuration;
			this.progressPlay.setProgress(0, this.workDuration);
		}
		
		private function netConnectionStatusHandler( event:NetStatusEvent ):void
		{
			log("netConnection: " + event.info.code + " (" + event.info.description + ")");
			if (event.info.code == "NetConnection.Connect.Success")
				this.play();
		}
		
		private function netStreamStatusHandler( event:NetStatusEvent ):void
		{
			log("netStream: " + event.info.code + " (" + event.info.description + ")");
			//log("bytes loaded: " + this.netStream.bytesLoaded.toString());
			//log("bytes total: " + this.netStream.bytesTotal.toString());
			//log("buffer length " + this.netStream.bufferLength.toString());
		
			
			if (event.info.code == "NetStream.Play.Reset") 
			{
				//initial subscription to stream
			}
			else if (event.info.code == "NetStream.Play.Start")
			{
				// start buffering
				this.lblLoading.visible  = true;
				this.isBuffering = true;
			}
			else if (event.info.code == "NetStream.Buffer.Full")
			{
				//done buffering
				this.lblLoading.visible  = false;
				this.isBuffering = false;
				this.resetProgress();
			}
			else if (event.info.code == "NetStream.Buffer.Flush")
			{
				// stopped in midstream
			}
			else if (event.info.code == "NetStream.Pause.Notify") 
			{
				//paused
			}
			else if (event.info.code == "NetStream.Unpause.Notify") 
			{
				//resumed
			}
			else if (event.info.code == "NetStream.Buffer.Empty") 
			{
				//end of stream
				this.stop();
				this.disconnect();
				this.resetDisplay();
			}
		}
		
		private function timeUpdaterElapsedHandler(e:TimerEvent):void 
		{
			this.displayTime();
			this.displayProgress();
		}
			
		private function connect():void 
		{
			if (this.netConnection == null) 
			{
				this.netConnection = new NetConnection();
				var uri:String = this.getUri();
				log ("connecting to " + uri);
				this.netConnection.connect(uri, this.streamId);
				this.netConnection.addEventListener(NetStatusEvent.NET_STATUS, netConnectionStatusHandler);				
			}
		}
		
		private function play():void 
		{
			this.isPlaying = true;
			log("play");
			this.netStream = new NetStream(this.netConnection);
			this.netStream.addEventListener(NetStatusEvent.NET_STATUS, netStreamStatusHandler);
			this.netStream.bufferTime = this.bufferTime;
			this.startTimers();
			this.netStream.soundTransform.volume = 1;
			this.netStream.play(this.streamId);
		}
		
		private function pause():void
		{
			this.isPaused = true;
			log("pause");
			this.netStream.pause();
		}
		
		private function  resume():void 
		{
			this.isPaused = false;
			log("resume");
			this.netStream.resume();			
		}
		
		private function stop():void 
		{
			this.isPlaying = false;
			this.stopTimers();
			if (this.netStream != null)
			{
				this.netStream.play(null);
				this.netStream.close();
				this.netStream = null;
			}
			this.displayProgress();
			this.displayTime();
		}
		
		private function disconnect():void 
		{
			if (this.netConnection != null)
			{
				this.netConnection.close();
				this.netConnection = null;
			}
		}
		
		private function startTimers():void
		{
			this.timeUpdater = new Timer(250);
			this.timeUpdater.addEventListener(TimerEvent.TIMER, timeUpdaterElapsedHandler);
			this.timeUpdater.start();
		}
		
		private function stopTimers():void 
		{
			this.timeUpdater.stop();
			this.timeUpdater = null;
		}
		
		private function getTimeString(totalSeconds:int):String
		{
			var minutes:int = Math.floor(totalSeconds/60);
			var seconds:int = totalSeconds % 60;
			return (minutes < 10 ? "0":"") + minutes.toString() + ":" + (seconds < 10 ? "0":"") + seconds.toString();
		}
		
		private function getUri():String 
		{
			var channel:Channel = ServerConfig.getChannel("my-rtmp");
			var uri:String = channel.endpoint;
			uri += "/Player";
			return uri;
		}
		
		private function log(message:String):void 
		{
			if (this.txtLog != null) {
				
				this.txtLog.text += "\n";
				this.txtLog.text += message;
				this.txtLog.text += "\n";
				this.callLater(scrollToBottom);
			}
		}
		
		private function scrollToBottom():void 
		{
			this.txtLog.verticalScrollPosition = this.txtLog.maxVerticalScrollPosition;
		}
	}
	
}