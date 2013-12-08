if (!window.Shared) { window.Shared = {}; }

Shared.UploadManager = new function() {
	var _my = this;

	_my.Uploading = false;
	_my.CurrentSWF = null;
	_my.UserMessages = [];

	_my.CancelAllUploads = function() {
		if (_my.Uploading) {
			_my.Uploading = false;
			if (_my.CurrentSWF) {
				_my.CurrentSWF.cancelUpload();
			}
		}
		if (_my.CurrentSWF) {
			_my.CurrentSWF.destroy();
			_my.CurrentSWF = null;
		}
		_my.UserMessages = [];
	};

	_my.FileDialogComplete = function(numFilesSelected, numFilesQueued) {
		var buttonPlaceholderDiv = $("#" + this.button_placeholder_id);
		var queued = Shared.UploadManager.CurrentSWF.getStats().files_queued;
		if (queued > 0) {
			$("#fileQueue").show();
			$("#btnUpload")
					.click(function(e) {
						e.preventDefault();
						buttonPlaceholderDiv.show();
						Shared.UploadManager.Uploading = true;
						Shared.UploadManager.CurrentSWF.startUpload();
					});
			$("#fileQueueList")
				.find(".lnkRemoveQueuedFile")
					.click(function(e) {
						e.preventDefault();
						var fileId = $(this).attr("id");
						Shared.UploadManager.CurrentSWF.cancelUpload(fileId);
						var table = $(this).parents("#fileQueueList");
						var tr = table.find("#row_" + fileId);
						tr.empty();
						Shared.UploadManager.CurrentSWF.setButtonDisabled(false);
						var queCount = Shared.UploadManager.CurrentSWF.getStats().files_queued;
						showFileQueueLabel(queCount);
						if (queCount == 0) {
							$("#fileQueue").hide();
						}
					});
			$("#uploadProgress").progressbar();
			$("#uploadProgress").progressbar('option', 'value', 0);
			if (queued == Shared.UploadManager.CurrentSWF.settings.file_queue_limit) {
				Shared.UploadManager.CurrentSWF.setButtonDisabled(true);
			}
			showFileQueueLabel(queued);
		} else {
			$("#fileQueue").hide();
			$("#fileQueueLabel").empty();
		}
	}

	function showFileQueueLabel(queued) {
		$("#fileQueueLabel").html(queued + " file" + (queued == 1 ? "" : "s") + " selected.");
	}

	// executes when a file is added to the queue
	_my.FileQueued = function(file) {
		var row = "<tr id='row_" + file.id + "'><td width='100%'>";
		row += file.name;
		row += "</td><td class='nowrap'>";
		row += Shared.Common.FileSize(file.size);
		row += "</td><td>";
		if (!Shared.Common.IsEmpty(file.type)) {
			row += (file.type.substring(0, 1) == '.' ? file.type.substring(1) : file.type);
		}
		row += "</td><td id='status_" + file.id + "'>Ready</td>";
		row += "<td><a href='#' id='" + file.id + "' class='lnkRemoveQueuedFile'>Remove</a></td></tr>";
		$("#fileQueueList").append(row);
	}

	//uploadStart is called immediately before the file is uploaded
	_my.UploadStart = function(file) {
		$(".lnkRemoveQueuedFile").hide();
		$("#uploadThrobber").show().html(Shared.Common.ProgressImage);
		$("#btnUpload").addClass("ui-state-disabled");
		this.setButtonDisabled(true);
		$("#row_" + file.id).css("font-weight", "bold")
			.addClass("ui-state-active");
		$("#status_" + file.id).html("Uploading...");
		$("#uploadProgress").progressbar('option', 'value', 0);
	}

	//The uploadProgress event is fired periodically by the Flash Control. This event is useful for providing UI updates on the page
	_my.UploadProgress = function(file, bytesLoaded) {
		var percent = Math.ceil((bytesLoaded / file.size) * 100);
		$("#uploadProgress").progressbar('option', 'value', percent)
		if (percent === 100) {
			$("#status_" + file.id).html("Processing...");
		} else {
			$("#status_" + file.id).html(percent + "%");
		}
	}

	//fired when the entire upload has been transmitted and the server returns a HTTP 200 status code
	// we expect serverData to be a json-formatted UserMessage object, which we add to the UserMessages array
	_my.UploadSuccess = function(file, serverData) {
		var userMessage = Shared.Common.ParseJson(serverData);
		_my.UserMessages.push(userMessage);
		//we could also user the file arg to manipulate the fileQueueList with a message...
	}

	//executes after a file is done processing.  
	//uploadComplete is always fired at the end of an upload cycle (after uploadError or uploadSuccess).
	_my.UploadComplete = function(file) {
		var fileQueueList = $("#fileQueueList");
		var row = fileQueueList.find("#row_" + file.id);
		row.css("font-weight", "normal")
			.removeClass("ui-state-active")
			.addClass("ui-state-disabled");
		row.find("#status_" + file.id).html("Done.");
		var removeLink = row.find(".lnkRemoveQueuedFile");
		removeLink.hide();
	}

	_my.FileQueueError = function(file, errorCode, message) {
		try {
			switch (errorCode) {
				case SWFUpload.QUEUE_ERROR.QUEUE_LIMIT_EXCEEDED:
					try {
						popupError("Sorry, you can only queue " + this.settings.file_queue_limit + " files at a time.");
					}
					catch (ex1) {
						this.debug(ex1);
					}
					break;
				case SWFUpload.QUEUE_ERROR.FILE_EXCEEDS_SIZE_LIMIT:
					try {
						popupError("Sorry, but" + file.name + " is too large.  The max size is " + this.settings.file_size_limit);
					}
					catch (ex1) {
						this.debug(ex1);
					}
					break;
				case SWFUpload.QUEUE_ERROR.ZERO_BYTE_FILE:
					try {
						popupError("The file you selected (" + file.name + ") is not a valid file.");
					}
					catch (ex2) {
						this.debug(ex2);
					}
				case SWFUpload.QUEUE_ERROR.INVALID_FILETYPE:
					try {
						popupError("Sorry, but only " + this.settings.file_types_description + " allowed.");
					}
					catch (ex3) {
						this.debug(ex3);
					}
				default:
					popupError(message);
					break;
			}
		} catch (ex3) {
			this.debug(ex3);
		}
	}

	_my.UploadError = function(file, errorCode, message) {
		global_uploadingImage = false;
		var progress;
		try {
			switch (errorCode) {
				case SWFUpload.UPLOAD_ERROR.FILE_CANCELLED:
					try {
						progress = new FileProgress(file, this.customSettings.upload_target);
						progress.setCancelled();
						progress.setStatus("Cancelled");
						progress.toggleCancel(false);
					}
					catch (ex1) {
						this.debug(ex1);
					}
					break;
				case SWFUpload.UPLOAD_ERROR.UPLOAD_STOPPED:
					try {
						progress = new FileProgress(file, this.customSettings.upload_target);
						progress.setCancelled();
						progress.setStatus("Stopped");
						progress.toggleCancel(true);
					}
					catch (ex2) {
						this.debug(ex2);
					}
				default:
					popupError(message);
					break;
			}
		} catch (ex3) {
			this.debug(ex3);
		}
	}
};
