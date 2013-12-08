if (!window.Shared) { window.Shared = {}; }

Shared.AudioWorkSingleList = new function() {
	var _my = this;

	_my.HijaxSingles = function() {
		$(".optionsListBox").click(function(e) {
			e.preventDefault();
			var target = $(e.target);
			if (target.is(".editSingle")) {
				handleAudioSingleEditClick(target);
			} else if (target.parent().is(".editSingle")) {
				handleAudioSingleEditClick(target.parent());
			} else if (target.is(".deleteSingle")) {
				handleAudioSingleDeleteClick(target);
			} else if (target.parent().is(".deleteSingle")) {
				handleAudioSingleDeleteClick(target.parent());
			} else if (target.is(".addToCollection")) {
				handleAddToCollectionClick(target);
			} else if (target.parent().is(".addToCollection")) {
				handleAddToCollectionClick(target.parent());
			} else if (target.is(".downloadSingle")) {
				Works.Index.HandleDownloadClick(target);
			} else if (target.parent().is(".downloadSingle")) {
				Works.Index.HandleDownloadClick(target.parent());
			}
		});

		$(".addSingle").click(function(e) {
			e.preventDefault();
			var target = $(e.target);
			handleAddSinglesClick(target);
		});
	};

	function handleAddToCollectionClick(target) {
		var url = $(target).attr("href");
		var audioWorkContainer = $(target).closest(".audioWorkContainer");
		var audioWorkEditContainer = audioWorkContainer.next(".audioWorkEditContainer");
		$.ajax({
			type: "GET",
			url: url,
			dataType: "html",
			cache: false,
			success: function(data) {
				audioWorkEditContainer.toggle('blind', null, 200);
				audioWorkEditContainer.html(data);
				var form = audioWorkEditContainer.find("form");
				AudioWork.AddToCollectionForm.HijaxForm(form);
				var cancel = form.find("a.cancel");
				cancel.click(function(e) {
					e.preventDefault();
					audioWorkEditContainer.html("");
					audioWorkEditContainer.hide();
				});
				form.submit(function() {
					handleAddToCollectionSubmit(form, audioWorkEditContainer);
					return false;
				});
			},
			error: function(XMLHttpRequest, textStatus, errorThrown) {
				audioWorkEditContainer.show().html(XMLHttpRequest.responseText);
			}
		});
	}

	function handleAddToCollectionSubmit(form, audioWorkEditContainer) {
		audioWorkEditContainer.html(Shared.Common.ProgressImage);
		var audioListContainer = audioWorkEditContainer.parents(".audioListContainer");
		var url = form.attr("action");
		$.ajax({
			type: "POST",
			url: url,
			data: form.serialize(),
			dataType: "json",
			success: function(data) {
				Works.Index.RefreshCollectionWorks();
				Works.Index.RefreshSingleWorks();
				Shared.Common.ShowUserMessage(data);
			},
			error: function(XMLHttpRequest, textStatus, errorThrown) {
				audioWorkEditContainer.html(XMLHttpRequest.responseText);
			}
		});

	}

	function handleAudioSingleEditClick(target) {
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
					handleAudioSingleEditSubmit(form, audioWorkEditContainer);
					return false;
				});

				Shared.Common.HijaxButtonHoverStates();
			},
			error: function(XMLHttpRequest, textStatus, errorThrown) {
				audioWorkEditContainer.html(XMLHttpRequest.responseText);
			}
		});
	}

	function handleAudioSingleEditSubmit(form, audioWorkEditContainer) {
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
					Works.Index.RefreshSingleWorks();
					Shared.Common.ShowUserMessage(data);
				},
				error: function(XMLHttpRequest, textStatus, errorThrown) {
					audioWorkEditContainer.html(XMLHttpRequest.responseText);
				}
			});
		}
	}


	function handleAudioSingleDeleteClick(target) {

		var title = target.parents(".audioWorkDetailContainer").find(".songTitle").html();
		var msg = 'Are you sure you want to delete ' + title + '?';

		$("#modalWindow").attr("title", "Confirm").html(msg).dialog({
			resizable: true,
			height: 200,
			width: 350,
			modal: true,
			overlay: { backgroundColor: '#000', opacity: 0.5 },
			buttons: {
				Cancel: function() { $(this).dialog('close').dialog('destroy'); },
				'Yes, delete': function() {
					$("#singleWorks").html(Shared.Common.ProgressImage);
					var url = $(target).attr("href");
					$.ajax({
						type: "POST",
						url: url,
						dataType: "json",
						success: function(data) {
							Shared.Common.ShowUserMessage(data);
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

	function handleAddSinglesClick(target) {
		Shared.UploadManager.CancelAllUploads();

		var uploadWindow = $("#uploadWindow");
		uploadWindow.attr("title", "Add Single Songs");
		uploadWindow.find("p").html("Select audio files to upload. You can upload Wave, FLAC, or MP3 files. You can upload 10 files at a time, and the maximum files size is 64MB.");

		var flashBox = uploadWindow.find(".flashBox");
		flashBox.empty();
		var buttonPlaceholderId = "up" + (new Date()).getTime();
		flashBox.append('<div class="uploadButton" id="' + buttonPlaceholderId + '"></div>');

		var url = $(target).attr("href");
		Shared.UploadManager.CurrentSWF = new SWFUpload({
			post_params: { "AUTHID": global_SWFParam, "ajax": true, "license": "by" },
			upload_url: url,
			file_size_limit: "64 MB",
			file_types: "*.wav;*.flac;*.mp3;",
			file_types_description: "Audio Files",
			file_upload_limit: "0",
			file_queue_limit: "10",
			button_placeholder_id: buttonPlaceholderId,
			button_image_url: '/content/SelectAudioFiles_hs.png',
			button_width: 138,
			button_height: 27,
			button_cursor: SWFUpload.CURSOR.HAND,
			flash_url: global_SWFUrl,
			custom_settings: { target_div: "uploadWindow", container_div: $("#singleWorks") },
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

	function queueComplete(numFilesUploaded) {
		$("#uploadThrobber").hide()
		Shared.Common.ShowUserMessages(Shared.UploadManager.UserMessages);
		//watch the order of these statements...
		$("#uploadWindow").dialog('close').dialog('destroy');
		Shared.UploadManager.CancelAllUploads();
		$("#singleWorks").html(Shared.Common.ProgressImage);
		Works.Index.RefreshSingleWorks();
	}

};