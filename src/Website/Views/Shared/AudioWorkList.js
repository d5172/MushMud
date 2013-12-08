if (!window.Shared) { window.Shared = {}; }

Shared.AudioWorkList = new function() {
	var _my = this;

	_my.HijaxLists = function() {

		$(".optionsListBox").click(function(e) {
			e.preventDefault();
			var target = $(e.target);
			if (target.is(".editSong")) {
				handleAudioEditClick(target);
			} else if (target.parent().is(".editSong")) {
				handleAudioEditClick(target.parent());
			} else if (target.is(".removeSong")) {
				handleAudioRemoveClick(target);
			} else if (target.parent().is(".removeSong")) {
				handleAudioRemoveClick(target.parent());
			} else if (target.is(".downloadSong")) {
				Works.Index.HandleDownloadClick(target);
			} else if (target.parent().is(".downloadSong")) {
				Works.Index.HandleDownloadClick(target.parent());
			}
		});

		$(".audioWorkContainer").hover(
            function() {
            	$(this).find(".sortButton").show();
            },
            function() {
            	$(this).find(".sortButton").hide();
            }
         );

        $("div.audioListContainer").each(function(i) {
			$(this).sortable("destroy").sortable({
				axis: "y",
				containment: $(this).parent(),
				opacity: 0.8,
				handle: $(".sortButton"),
				stop: function(event, ui) {
					var items = $(this).find(".audioWorkContainer");
					var ids = '';
					for (var i = 0; i < items.length; i++) {
						var container = $(items[i]);
						ids += container.attr("id") + ';';
					}
					var audioListContainer = $(this);
					var url = audioListContainer.parent().find("input.audioSortRoute").val();
					$.ajax({
						type: "POST",
						url: url,
						data: "ids=" + ids,
						dataType: "json",
						success: function(data) {
							Shared.AudioWorkList.RefreshAudioWorks(audioListContainer);
						},
						error: function(XMLHttpRequest, textStatus, errorThrown) {
							$(this).html(XMLHttpRequest.responseText);
						}
					});
				}
			})
		});

		$(".songDetailMore").click(function(e) {
			e.preventDefault();
			$(this).next(".songDetail").fadeIn();
		});

		$(".songDetailClose").click(function(e) {
			e.preventDefault();
			$(this).parents(".songDetail").hide();
		});
	}

	_my.RefreshAudioWorks = function(audioListContainer) {
		var url = $(audioListContainer).parent().find("input.audioListRoute").val();
		$.ajax({
			type: "GET",
			url: url,
			dataType: "html",
			cache: false,
			success: function(data) {
				audioListContainer.html(data);
				Works.Index.HijaxAll();
				Shared.AudioWorkList.HijaxLists();
			},
			error: function(XMLHttpRequest, textStatus, errorThrown) {
				audioListContainer.html(XMLHttpRequest.responseText);
			}
		});
	}

	_my.HandleAddSongsClick = function(target) {
		Shared.UploadManager.CancelAllUploads();
		var collectionBox = target.parents(".collectionBox");

		var audioListContainerId = collectionBox.find(".audioListContainer").attr("id");

		var uploadWindow = $("#uploadWindow");
		var collectionTitle = collectionBox.find(".collectionTitle").html();
		uploadWindow.attr("title", "Add Songs to " + collectionTitle);
		uploadWindow.find("p").html("Select MP3 files to upload. You can upload 10 files at a time, and the maximum files size is 20MB.");

		var flashBox = uploadWindow.find(".flashBox");
		flashBox.empty();
		var buttonPlaceholderId = "up" + (new Date()).getTime();
		flashBox.append('<div class="uploadButton" id="' + buttonPlaceholderId + '"></div>');

		var url = $(target).attr("href");
		Shared.UploadManager.CurrentSWF = new SWFUpload({
			post_params: { "AUTHID": global_SWFParam, "ajax": true },
			upload_url: url,
			file_size_limit: "20 MB",
			file_types: "*.mp3;",
			file_types_description: "MP3 Files",
			//file_types: "*.wav;*.flac;*.mp3;",
			//file_types_description: "Audio Files",
			
			file_upload_limit: "0",
			file_queue_limit: "10",
			button_placeholder_id: buttonPlaceholderId,
			button_image_url: '/content/SelectAudioFiles_hs.png',
			button_width: 138,
			button_height: 27,
			button_cursor: SWFUpload.CURSOR.HAND,
			flash_url: global_SWFUrl,
			custom_settings: { target_div: "uploadWindow", container_div: audioListContainerId },
			file_dialog_complete_handler: Shared.UploadManager.FileDialogComplete,
			file_queued_handler: Shared.UploadManager.FileQueued,
			file_queue_error_handler: Shared.UploadManager.FileQueueError,
			upload_start_handler: Shared.UploadManager.UploadStart,
			upload_progress_handler: Shared.UploadManager.UploadProgress,
			upload_error_handler: Shared.UploadManager.UploadError,
			upload_success_handler: Shared.UploadManager.UploadSuccess,
			upload_complete_handler: Shared.UploadManager.UploadComplete,
			queue_complete_handler: queueComplete
		});
		$("#fileQueue").hide();
		$("#fileQueueList").empty();
		$("#btnUpload").removeClass("ui-state-disabled");

		uploadWindow.dialog({
			resizable: true,
			height: 400,
			width: 550,
			modal: true,
			overlay: {
				backgroundColor: '#000',
				opacity: 0.5
			},
			buttons: {
				Cancel: function() {
					Shared.UploadManager.CancelAllUploads();
					$("#uploadThrobber").hide();
					$(this).dialog('close').dialog('destroy');
				}
			},
			close: function(e, u) {
				Shared.UploadManager.CancelAllUploads();
				$("#uploadThrobber").hide();
				$(this).dialog('destroy');
			}
		});
	}

	//executes after all files are done uploading and processing
	function queueComplete(numFilesUploaded) {
		$("#uploadThrobber").hide()
		Shared.Common.ShowUserMessages(Shared.UploadManager.UserMessages);
		//watch the order of these statements...
		var targetDiv = $("#" + this.customSettings.target_div);
		var audioListContainer = $("#" + this.customSettings.container_div);
		$("#uploadWindow").dialog('close').dialog('destroy');
		Shared.UploadManager.CancelAllUploads();
		audioListContainer.html(Shared.Common.ProgressImage);
		Shared.AudioWorkList.RefreshAudioWorks(audioListContainer);
	}

	function handleAudioEditClick(target) {
		var url = $(target).attr("href");

		var audioWorkContainer = $(target).closest(".audioWorkContainer");
		var audioWorkEditContainer = audioWorkContainer.next(".audioWorkEditContainer");

		audioWorkContainer.hide();
		audioWorkEditContainer.show().html(Shared.Common.ProgressImage);

		$.ajax({
			type: "GET",
			url: url,
			dataType: "html",
			cache: false,
			success: function(data) {
				audioWorkEditContainer.hide();
				audioWorkEditContainer.html(data);
				audioWorkEditContainer.fadeIn('fast');
				var form = audioWorkEditContainer.find("form");
				AudioWork.AudioWorkForm.HijaxForm(form);
				var cancel = form.find("a.cancel");
				cancel.click(function(e) {
					e.preventDefault();
					audioWorkEditContainer.html("");
					audioWorkEditContainer.hide();
					audioWorkContainer.show();
				});
				form.submit(function() {
					handleAudioEditSubmit(form, audioWorkEditContainer);
					return false;
				});

				Shared.Common.HijaxButtonHoverStates();
			},
			error: function(XMLHttpRequest, textStatus, errorThrown) {
				audioWorkEditContainer.html(XMLHttpRequest.responseText);
			}
		});
	}

	function handleAudioEditSubmit(form, audioWorkEditContainer) {
		if (form.valid()) {
			audioWorkEditContainer.html(Shared.Common.ProgressImage);
			var audioListContainer = audioWorkEditContainer.parents(".audioListContainer");
			var url = form.attr("action");
			$.ajax({
				type: "POST",
				url: url,
				data: form.serialize(),
				dataType: "json",
				success: function(data) {
					Shared.AudioWorkList.RefreshAudioWorks(audioListContainer);
					Shared.Common.ShowUserMessage(data);
				},
				error: function(XMLHttpRequest, textStatus, errorThrown) {
					audioWorkEditContainer.html(XMLHttpRequest.responseText);
				}
			});
		}
	}

	function handleAudioRemoveClick(target) {
		var audioListContainer = target.parents(".audioListContainer");

		var title = target.parents(".audioWorkDetailContainer").find(".songTitle").html();
		var msg = 'Are you sure you want to remove ' + title + ' from the collection?';

		$("#modalWindow").attr("title", "Confirm").html(msg).dialog({
			resizable: true,
			height: 200,
			width: 350,
			modal: true,
			overlay: { backgroundColor: '#000', opacity: 0.5 },
			buttons: {
				Cancel: function() { $(this).dialog('close').dialog('destroy'); },
				'Yes, remove': function() {
					audioListContainer.html(Shared.Common.ProgressImage);
					var url = $(target).attr("href");
					$.ajax({
						type: "POST",
						url: url,
						dataType: "json",
						success: function(data) {
							Shared.Common.ShowUserMessage(data);
							Shared.AudioWorkList.RefreshAudioWorks(audioListContainer);
							$("#singleWorks").html(Shared.Common.ProgressImage);
							Works.Index.RefreshSingleWorks();
						},
						error: function(XMLHttpRequest, textStatus, errorThrown) {
							audioListContainer.html(XMLHttpRequest.responseText);
						}
					});
					$(this).dialog('close').dialog('destroy');
				}
			},
			close: function(e, u) { $(this).dialog('destroy'); }
		}
    );
	}
};