if (!window.Shared) { window.Shared = {}; }

Shared.PlayerManager = new function() {
	var _my = this;

	/// Reference to the ready jPlayer,
	var _playerInstance;

	/// Reference to the currently active player ui-widget
	var _playerInstance_UI;

	/// Flag to indicate if playback should start when a callback executes
	var _startPlayback = false;

	/// Flag to indicate if playback should loop
	var _repeatPlayer = false;

	/// Array of trackRows currently in context
	var _playList;

	/// Current _playList index
	var _playListIndex;

	/// Id of the traklist currently loaded
	var _currentTrackListId = "";

	/// Wires up the ui-widgets in all players on the page
	_my.SetupPlayers = function() {
		//alert("setupplayers");
		var useHtml5Audio = true;
		if ($.browser.webkit) useHtml5Audio = false;  //turning off HTML 5 due to various issues with Chrome.
		$("#jquery_jplayer").jPlayer({
			ready: function() {
				//alert("jplayer ready");
				_playerInstance = this.element; //$(this);

				//alert("swf: " + _playerInstance.jPlayer("getData", "swf"));
				//alert("canPlayMP3 : " + _playerInstance.jPlayer("getData", "canPlayMP3"));
				//alert("html5 : " + _playerInstance.jPlayer("getData", "html5"));
				//alert("usingFlash  : " + _playerInstance.jPlayer("getData", "usingFlash"));
				//alert("audio  : " + _playerInstance.jPlayer("getData", "audio"));
			}
			, oggSupport: false 	//TODO: make ogg files available
			, volume: 100
			, nativeSupport: useHtml5Audio
			, swfPath: "/js"
		});

		$("a.expandTrackList").click(function(e) {
			e.preventDefault();
			var toExpand = $(this).parents(".expandContainer").find(".expandable");
			toExpand.toggle("blind", null, 200);
			if ($(this).hasClass("ui-icon-triangle-1-e")) {
				$(this).removeClass("ui-icon-triangle-1-e");
				$(this).addClass("ui-icon-triangle-1-s");
				if (Shared.Common.IsEmpty(toExpand.html())) {
					toExpand.html(Shared.Common.ProgressImage);
					var url = $(this).attr("href");
					$.ajax({
						type: "GET",
						url: url,
						dataType: "html",
						cache: false,
						success: function(data) {
							toExpand.html(data);
							hijaxTrackList(toExpand);
							if (_startPlayback) {
								_startPlayback = false;
								loadPlayList(toExpand);
								startPlayList();
							}
						},
						error: function(XMLHttpRequest, textStatus, errorThrown) {
							toExpand.html(XMLHttpRequest.responseText);
						}
					});
				}
			} else {
				$(this).removeClass("ui-icon-triangle-1-s");
				$(this).addClass("ui-icon-triangle-1-e");
			}
		});

		$("a.playCollectionButton").click(function(e) {
			e.preventDefault();
			var playButton = $(this);
			if (playButton.hasClass("stopped") || playButton.hasClass("paused")) {
				playCollection(playButton);
			} else if (playButton.hasClass("playing")) {
				pauseCollection(playButton);
			}
		});

		$("a.playSingleButton").click(function(e) {
			e.preventDefault();
			var playButton = $(this);
			if (playButton.hasClass("stopped") || playButton.hasClass("paused")) {
				playSingle(playButton);
			} else if (playButton.hasClass("playing")) {
				pauseCollection(playButton);
			}
		});
	};

	/// Starts (or resumes) playback of the single and changes state of button
	function playSingle(playButton) {
		var trackList = playButton.parents(".collectionContainer").find(".trackList");
		//play
		if (playButton.hasClass("paused")) {
			resumeTrackRow(_playList[_playListIndex]);
		} else {
			//start playback
			stopTrackList();
			loadPlayList(trackList);
			startPlayList();
		}

		//adjust the button
		playButton_Playing(playButton);
	}

	/// Starts (or resumes) playback of the collection and changes state of button
	function playCollection(playButton) {
		var trackList = playButton.parents(".collectionContainer").find(".trackList");
		//play
		if (playButton.hasClass("paused")) {
			resumeTrackRow(_playList[_playListIndex]);
		} else {
			// show tracklist if hidden, or just start playback
			stopTrackList();
			if (!trackList.is(":visible")) {
				_startPlayback = true;
				trackList.parents(".expandContainer").find(".expandTrackList").click();
			} else {
				loadPlayList(trackList);
				startPlayList();
			}
		}

		//adjust the button
		playButton_Playing(playButton);
	}

	/// Pauses playback and changes the button back to a "play" button
	function pauseCollection(playButton) {
		//pause the trackList
		pauseTrackRow(_playList[_playListIndex]);

		//adjust the button
		playButton_Paused(playButton);
	}

	/// Stops playback, removes playlist from memory 
	/// and adjusts button
	function stopCollection(playButton) {

		//stop the trackList
		stopTrackList();

		//adjust the button
		playButton_Stopped(playButton);
	}

	/// Changes button to a "pause" button
	function playButton_Playing(playButton) {
		if (playButton.hasClass("stopped") || playButton.hasClass("paused")) {
			//track state with class
			playButton.removeClass("stopped").removeClass("paused").addClass("playing");

			//adjust icon and label
			var icon = playButton.find("span.ui-icon");
			icon.addClass("ui-icon-pause");
			icon.removeClass("ui-icon-play");
			icon.next("span").html("Pause");

			//change title
			playButton.attr("title", "Pause");

			//set priority
			playButton.addClass("ui-priority-secondary");
		}
	}

	/// Changes play button to a Pause button
	function playButton_Paused(playButton) {
		if (playButton.hasClass("playing")) {
			//track state with class
			playButton.removeClass("playing").addClass("paused");

			//adjust icon and label
			var icon = playButton.find("span.ui-icon");
			icon.removeClass("ui-icon-pause");
			icon.addClass("ui-icon-play");
			icon.next("span").html("Play");

			//change title
			playButton.attr("title", "Resume");

			//set priority
			playButton.removeClass("ui-priority-secondary");
		}
	}

	/// Changes play button back to a "play" button
	function playButton_Stopped(playButton) {
		if (playButton.hasClass("playing") || playButton.hasClass("paused")) {
			//track state with class
			playButton.removeClass("playing").removeClass("paused").addClass("stopped");

			//adjust icon and label
			var icon = playButton.find("span.ui-icon");
			icon.removeClass("ui-icon-pause");
			icon.addClass("ui-icon-play");
			icon.next("span").html("Play");

			//change title
			playButton.attr("title", "Play");

			//set priority
			playButton.removeClass("ui-priority-secondary");
		}
	}

	/// Starts playing the first track
	function startPlayList() {
		//play first track
		if (_playList.length > 0) {
			playTrackRow(_playList[0]);
		}
	}

	/// Loads the tracks in the trackList into the playlist
	function loadPlayList(trackList) {
		//init _playList
		_playList = new Array();
		_playListIndex = 0;

		//add each trackRow in trackList to the playlist, and add an attribute to each trackRow to track it's index
		trackList
            .find(".trackRow")
            .each(
            function(i) {
            	var trackRow = $(this);
            	trackRow.attr("trackIndex", i);
            	_playList[i] = trackRow;
            });

		//track current tracklist
		_currentTrackListId = trackList.attr("id");
	}

	/// Finds the current trackRow and stops it,
	/// clears the _playList and _playListIndex, 
	/// and hides the player ui-widgets
	function stopTrackList() {
		if (_playList != null) {
			stopTrackRow(_playList[_playListIndex]);
		}
		_playList = null;
		_playListIndex = 0;
		if (_playerInstance_UI != null && _playerInstance_UI.is(":visible")) {
			_playerInstance_UI.fadeOut(500);
		}
		_currentTrackListId = "";
	}

	/// Sets the player to the trackRow's file, starts playback, 
	/// and changes the play button in the row to "playing" state
	function playTrackRow(trackRow) {

		//set _playListIndex to match the current track's index
		_playListIndex = parseInt(trackRow.attr("trackIndex"));

		//stop all other rows
		$(".trackRow").not(trackRow).each(function(i) {
			stopTrackRow($(this));
		});

		//wire up the player (TODO: only if not already connected)
		var player = $(trackRow).parents(".collectionContainer").find(".player");
		connectPlayer(player);

		// set the player to the file
		var playTrackButton = $(trackRow).find(".playTrackButton")
		var url = playTrackButton.attr("href");
		var mp3Url = url + "/MP3";
		//var oggUrl = url + "/OGG";

		_playerInstance.jPlayer("setFile", mp3Url);

		//alert("diag.src: " + _playerInstance.jPlayer("getData", "diag.src"));

		//cue up the next track
		_playerInstance.jPlayer("onSoundComplete", function() {
			if (_playList.length - 1 > _playListIndex) {
				playTrackRow(_playList[_playListIndex + 1]);
			} else {
				if (_repeatPlayer) {
					playTrackRow(_playList[0]);
				} else {
					stopTrackList();
				}
			}
		});

		//start playback
		_playerInstance.jPlayer("play");

		//alert("diag.isPlaying: " + _playerInstance.jPlayer("getData", "diag.isPlaying"));
		//alert("diag.loadPercent: " + _playerInstance.jPlayer("getData", "diag.loadPercent"));

		//adjust the button
		playButton_Playing(playTrackButton);

		//show the row as active
		activateTrackRow(trackRow, true);

		//make sure main is playing
		var playCollectionButton = player.parents(".playerContainer").find(".playCollectionButton");
		if (!playCollectionButton.hasClass("playing")) {
			playButton_Playing(playCollectionButton);
		}
	}

	/// Pauses play back, and changes the buttons back to "paused" state
	function pauseTrackRow(trackRow) {
		//pause
		_playerInstance.jPlayer("pause");

		//adjust the button
		var playTrackButton = $(trackRow).find(".playTrackButton");
		playButton_Paused(playTrackButton);

		//make sure main button is paused
		var player = $(trackRow).parents(".collectionContainer").find(".player");
		var playCollectionButton = player.parents(".playerContainer").find(".playCollectionButton");
		if (!playCollectionButton.hasClass("paused")) {
			playButton_Paused(playCollectionButton);
		}
	}

	/// Resumes playback and changes buttons to "playing" state
	function resumeTrackRow(trackRow) {
		//pause
		_playerInstance.jPlayer("play");

		//adjust the button
		var playTrackButton = $(trackRow).find(".playTrackButton");
		playButton_Playing(playTrackButton);

		//make sure main is playing
		var player = $(trackRow).parents(".collectionContainer").find(".player");
		var playCollectionButton = player.parents(".playerContainer").find(".playCollectionButton");
		if (!playCollectionButton.hasClass("playing")) {
			playButton_Playing(playCollectionButton);
		}
	}

	/// Stops playback and changes buttons to "stopped" state
	function stopTrackRow(trackRow) {
		try {
			//chrome: invalid_state_err dom exception 11
			_playerInstance.jPlayer("stop");
		} catch (ex) { }
		var playTrackButton = $(trackRow).find(".playTrackButton");
		playButton_Stopped(playTrackButton);

		activateTrackRow(trackRow, false);

		//make sure main is stopped
		var player = $(trackRow).parents(".collectionContainer").find(".player");

		var playerButton = player.parents(".playerContainer").find(".playerButton");
		if (!playerButton.hasClass("stopped")) {
			playButton_Stopped(playerButton);
		}
	}

	/// Shows the trackRow as activated or not.
	function activateTrackRow(trackRow, activated) {

		var audioWorkTitle = trackRow.find(".audioWorkTitle");

		audioWorkTitle.css("font-weight", activated ? "bold" : "normal");

		//make sure all others are normal
		$(".audioWorkTitle").not(audioWorkTitle).css("font-weight", "normal");
	}

	/// Wires up the _playerInstance to the correct ui widgets, 
	/// and makes it visible if hidden
	function connectPlayer(player) {

		//track the current player_ui instance (so we can kill it later without having to find it)
		_playerInstance_UI = player;

		//if it's not visible, show it and wire up the buttons
		if (!player.is(":visible")) {
			player.fadeIn(500);
		}

		//wire up the buttons if not already
		if (!player.hasClass("wired")) {

			player.find(".stopButton").click(function(e) {
				e.preventDefault();
				stopTrackList();
			});
			if (_playList.length > 1) {
				player.find(".prevButton").click(function(e) {
					e.preventDefault();
					if (_playListIndex > 0) {
						playTrackRow(_playList[_playListIndex - 1]);
					}
				});
				player.find(".nextButton").click(function(e) {
					e.preventDefault();
					if (_playList.length - 1 > _playListIndex) {
						playTrackRow(_playList[_playListIndex + 1]);
					}
				});
			} else {
				player.find(".prevButton").hide();
				player.find(".nextButton").hide();
			}
			player.find(".repeatButton").click(function(e) {
				e.preventDefault();
				$(this).toggleClass("ui-state-active");
				_repeatPlayer = $(this).hasClass("ui-state-active");
			});
			player.addClass("wired");
		}

		var playProgress = player.find(".playProgress");
		var playTimeLabel = player.find(".playTime");
		var totalTimeLabel = player.find(".totalTime");
		var loadProgress = player.find(".loadProgress");

		loadProgress.progressbar();
		playProgress.progressbar();
		_playerInstance.jPlayer("onProgressChange",
              function(loadPercent, playedPercentRelative, playedPercentAbsolute, playedTime, totalTime) {
              	//progress bars
              	var loadPercentInt = parseInt(loadPercent);
              	var playedPercentAbsoluteInt = parseInt(playedPercentAbsolute);
              	loadProgress.progressbar('option', 'value', loadPercentInt);
              	playProgress.progressbar('option', 'value', playedPercentAbsoluteInt);

              	//labels
              	playTimeLabel.text($.jPlayer.convertTime(playedTime));
              	totalTimeLabel.text($.jPlayer.convertTime(totalTime));
              }
        );
	}

	/// Wires up the links and buttons in the trackList
	function hijaxTrackList(trackList) {

		$(trackList).find(".audioWorkTitle").click(function(e) {
			e.preventDefault();
			var audioWorkDetailContainer = $(this).next(".audioWorkDetailContainer");
			audioWorkDetailContainer.toggle("blind", null, 200);
			if (Shared.Common.IsEmpty(audioWorkDetailContainer.html())) {
				audioWorkDetailContainer.html(Shared.Common.ProgressImage);
				var url = $(this).attr("href");
				$.ajax({
					type: "GET",
					url: url,
					dataType: "html",
					cache: false,
					success: function(data) {
						audioWorkDetailContainer.html(data);
					},
					error: function(XMLHttpRequest, textStatus, errorThrown) {
						audioWorkDetailContainer.html(XMLHttpRequest.responseText);
					}
				});
			}
		});

		$(trackList).find(".playTrackButton").click(function(e) {
			e.preventDefault();
			if (trackList.attr("id") != _currentTrackListId) {
				stopTrackList();
			}
			if (_playList == null) {
				loadPlayList(trackList);
			}
			var playButton = $(this);
			var trackRow = $(this).parents(".trackRow");
			if (playButton.hasClass("stopped")) {
				playTrackRow(trackRow);
			} else if (playButton.hasClass("playing")) {
				pauseTrackRow(trackRow);
			} else if (playButton.hasClass("paused")) {
				resumeTrackRow(trackRow);
			}
		});


		Shared.WorkList.HijaxDownloadLinks();

		Shared.Common.HijaxButtonHoverStates();
	}
}