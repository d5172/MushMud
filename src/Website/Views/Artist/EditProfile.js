if (!window.Artist) { window.Artist = {}; }

Artist.EditProfile = new function() {
	var _my = this;

	_my.HijaxAll = function() {
		Shared.OptionsLink.HijaxOptionsLinks();

		$(".optionsListBox").click(function(e) {
			e.preventDefault();
			var target = $(e.target);
			if (target.is(".editProfile")) {
				handleEditProfileClick(target);
			} else if (target.parent().is(".editProfile")) {
				handleEditProfileClick(target.parent());
			} else if (target.is(".editImage")) {
				handleEditImageClick(target);
			} else if (target.parent().is(".editImage")) {
				handleEditImageClick(target.parent());
			}
		});

		$("#emptyBio").click(function(e) {
			$(".editProfile").click();
		});

	};

	function handleEditProfileClick(target) {
		var viewBox = $("#viewBox");
		var editBox = $("#editBox");
		viewBox.hide();
		editBox.show();
		editBox.html(Shared.Common.ProgressImage);
		var url = target.attr("href");
		$.ajax({
			type: "GET",
			url: url,
			dataType: "html",
			cache: false,
			success: function(data) {
				editBox.hide();
				editBox.html(data);
				editBox.fadeIn('fast');
				var form = editBox.find("form");
				Artist.BioForm.HijaxForm(form);
				var cancel = form.find("a.cancel");
				cancel.click(function(e) {
					e.preventDefault();
					handleEditProfileCancelClick();
				});
				form.submit(function() {
					handleProfileSubmit(form);
					return false;
				});
				Shared.Common.HijaxButtonHoverStates();
			},
			error: function(XMLHttpRequest, textStatus, errorThrown) {
				editBox.html(XMLHttpRequest.responseText);
			}
		});
	}

	function handleEditProfileCancelClick() {
		$("#editBox").empty().hide();
		$("#viewBox").show();
	}

	function handleProfileSubmit(form) {
		if (form.valid()) {
			var viewBox = $("#viewBox");
			var editBox = $("#editBox");
			editBox.html(Shared.Common.ProgressImage);
			var url = form.attr("action");
			if (form.valid()) {
				$.ajax({
					type: "POST",
					url: url,
					data: form.serialize(),
					dataType: "json",
					success: function(data) {
						$.ajax({
							type: "GET",
							dataType: "html",
							cache: false,
							url: global_ArtistBioUrl,
							success: function(data) {
								editBox.hide();
								viewBox.show();
								viewBox.html(data);
							},
							error: function(XMLHttpRequest, textStatus, errorThrown) {
								editBox.html(XMLHttpRequest.responseText);
							}
						});
						Shared.Common.ShowUserMessage(data);
					},
					error: function(XMLHttpRequest, textStatus, errorThrown) {
						editBox.html(XMLHttpRequest.responseText);
					}
				});
			}
		}
	}

	function handleEditImageClick(target) {
		Shared.UploadManager.CancelAllUploads();

		var imgContainer = $("#imgBox");
		var targetImageId = imgContainer.find("img").attr("id");

		var uploadWindow = $("#uploadWindow");
		uploadWindow.attr("title", "Change the picture");
		uploadWindow.find("p").html("Select a JPEG image to upload.  This will be your profile picture.  Maximum file size allowed is 3MB.");

		var flashBox = uploadWindow.find(".flashBox");
		flashBox.empty();
		var buttonPlaceholderId = "up" + (new Date()).getTime();
		flashBox.append('<div class="uploadButton" id="' + buttonPlaceholderId + '"></div>');

		var url = $(target).attr("href");
		Shared.UploadManager.CurrentSWF = new SWFUpload({
			post_params: { "AUTHID": global_SWFParam, "ajax": true },
			upload_url: url,
			file_size_limit: "3 MB",
			file_types: "*.jpg",
			file_types_description: "JPG Images",
			file_upload_limit: "0",
			file_queue_limit: "1",
			button_placeholder_id: buttonPlaceholderId,
			button_image_url: '/content/browse_file_hs.png',
			button_width: 151,
			button_height: 27,
			button_cursor: SWFUpload.CURSOR.HAND,
			flash_url: global_SWFUrl,
			custom_settings: { target_div: "uploadWindow" },
			file_dialog_complete_handler: Shared.UploadManager.FileDialogComplete,
			file_queued_handler: Shared.UploadManager.FileQueued,
			file_queue_error_handler: Shared.UploadManager.FileQueueError,
			upload_start_handler: Shared.UploadManager.UploadStart,
			upload_progress_handler: Shared.UploadManager.UploadProgress,
			upload_error_handler: Shared.UploadManager.UploadError,
			upload_complete_handler: Shared.UploadManager.UploadComplete,
			queue_complete_handler: profileImageQueueComplete,
			upload_success_handler: profileImageUploadSucces
		});
		$("#fileQueue").hide();
		$("#fileQueueList").empty();
		$("#btnUpload").removeClass("ui-state-disabled");

		uploadWindow.dialog({
			resizable: true,
			height: 375,
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

	function profileImageQueueComplete(numFilesUploaded) {
		$("#uploadThrobber").hide();
		$("#uploadWindow").dialog('close').dialog('destroy');
		Shared.UploadManager.CancelAllUploads();
	}

	function profileImageUploadSucces(file, serverData) {
		//get the target img node
		var imgBox = $("#imgBox");
		var img = imgBox.find("img");

		//set src to temp image
		img.attr("src", Shared.Common.ProgressImageUrl);

		//recieve the url to the new image
		var response = Shared.Common.ParseJson(serverData);

		//set the img src to new url
		img.attr("src", response.ImageUrl);

		//show a generic sucess message		
		Shared.Common.ShowUserMessage({ Type: 1, Message: "Picture was changed." });
	}
}

//JQuery Ready
$(function() {
	Artist.EditProfile.HijaxAll();
});
