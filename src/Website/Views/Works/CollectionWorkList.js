if (!window.Works) { window.Works = {}; }

Works.CollectionWorkList = new function() {
	var _my = this;

	_my.HijaxCollections = function() {

		$(".optionsListBox").click(function(e) {
			e.preventDefault();
			var target = $(e.target);
			if (target.is(".editCollection")) {
				handleCollectionEditClick(target);
			} else if (target.parent().is(".editCollection")) {
				handleCollectionEditClick(target.parent());
			} else if (target.is(".deleteCollection")) {
				handleCollectionDeleteClick(target);
			} else if (target.parent().is(".deleteCollection")) {
				handleCollectionDeleteClick(target.parent());
			} else if (target.is(".editImage")) {
				handleImgEditClick(target);
			} else if (target.parent().is(".editImage")) {
				handleImgEditClick(target.parent());
			} else if (target.is(".deleteImage")) {
				handleImgDeleteClick(target);
			} else if (target.parent().is(".deleteImage")) {
				handleImgDeleteClick(target.parent());
			} else if (target.is(".addSongs")) {
				Shared.AudioWorkList.HandleAddSongsClick(target);
			} else if (target.parent().is(".addSongs")) {
				Shared.AudioWorkList.HandleAddSongsClick(target.parent());
			}
		});

		$(".addTracks").click(function(e) {
			e.preventDefault();
			var target = $(e.target);
			Shared.AudioWorkList.HandleAddSongsClick(target);
		});

		$(".addCollection").click(function(e) {
			e.preventDefault();
			var colFormContainer = $(this).parent().parent().find("div.colFormContainer");
			colFormContainer.html(Shared.Common.ProgressImage);
			$.ajax({
				type: "GET",
				url: this.href,
				dataType: "html",
				cache: false,
				success: function(data) {
					colFormContainer.html(data);
					var form = colFormContainer.find("form");
					CollectionWork.CollectionWorkForm.HijaxForm(form);
					var cancel = form.find("a.cancel");
					cancel.click(function(e) {
						e.preventDefault();
						colFormContainer.html("");
					});
					form.submit(function() {
						handleCollectionSubmit(form, colFormContainer);
						return false;
					});

					Shared.Common.HijaxButtonHoverStates();
				},
				error: function(XMLHttpRequest, textStatus, errorThrown) {
					colFormContainer.html(XMLHttpRequest.responseText);
				}
			});
			return false;
		});
	}

	function handleCollectionEditClick(target) {
		var collectionBox = target.parents(".collectionBox");
		var collectionViewContainer = collectionBox.find("div.collectionViewContainer");
		collectionViewContainer.hide();
		var collectionEditContainer = collectionBox.find("div.collectionEditContainer");
		collectionEditContainer.show();
		collectionEditContainer.html(Shared.Common.ProgressImage);
		var url = target.attr("href");
		$.ajax({
			type: "GET",
			url: url,
			dataType: "html",
			cache: false,
			success: function(data) {
				collectionEditContainer.hide();
				collectionEditContainer.html(data);
				collectionEditContainer.fadeIn('fast');
				var form = collectionEditContainer.find("form");
				CollectionWork.CollectionWorkForm.HijaxForm(form);
				var cancel = form.find("a.cancel");
				cancel.click(function(e) {
					e.preventDefault();
					var target = $(e.target);
					handleCollectionEditCancelClick(target);
				});
				form.submit(function() {
					handleCollectionSubmit(form, collectionEditContainer);
					return false;
				});

				Shared.Common.HijaxButtonHoverStates();
			},
			error: function(XMLHttpRequest, textStatus, errorThrown) {
				collectionEditContainer.html(XMLHttpRequest.responseText);
			}
		});
	}

	function handleCollectionEditCancelClick(target) {
		var collectionBox = target.parents(".collectionBox");
		var collectionEditContainer = collectionBox.find("div.collectionEditContainer");
		collectionEditContainer.hide();
		var collectionViewContainer = collectionBox.find("div.collectionViewContainer");
		collectionViewContainer.show();
	}

	function handleCollectionSubmit(form, colFormContainer) {
		if (form.valid()) {
			colFormContainer.html(Shared.Common.ProgressImage);
			var url = form.attr("action");
			if (form.valid()) {
				$.ajax({
					type: "POST",
					url: url,
					data: form.serialize(),
					dataType: "json",
					success: function(data) {
						colFormContainer.empty();
						Works.Index.RefreshCollectionWorks();
						Shared.Common.ShowUserMessage(data);
					},
					error: function(XMLHttpRequest, textStatus, errorThrown) {
						colFormContainer.html(XMLHttpRequest.responseText);
					}
				});
			}
		}
	}

	function handleCollectionDeleteClick(target) {
		var collectionBox = target.parents(".collectionBox");
		var title = collectionBox.find(".collectionTitle").html();
		var msg = 'Are you sure you want to delete ' + title + '?<p>(This will make the songs into singles.)</p>';

		$("#modalWindow").attr("title", "Confirm").html(msg).dialog({
			resizable: true,
			height: 250,
			width: 350,
			modal: true,
			overlay: {
				backgroundColor: '#000',
				opacity: 0.5
			},
			buttons: {
				Cancel: function() {
					$(this).dialog('close').dialog('destroy');
				},
				'Yes, delete': function() {
					var collectionEditContainer = collectionBox.find("div.collectionEditContainer");
					collectionEditContainer.show();
					collectionEditContainer.html(Shared.Common.ProgressImage);
					var url = $(target).attr("href");
					$.ajax({
						type: "POST",
						url: url,
						dataType: "json",
						success: function(data) {
							Shared.Common.ShowUserMessage(data);
							Works.Index.RefreshCollectionWorks();
							$("#singleWorks").html(Shared.Common.ProgressImage);
							Works.Index.RefreshSingleWorks();
						},
						error: function(XMLHttpRequest, textStatus, errorThrown) {
							collectionEditContainer.html(XMLHttpRequest.responseText);
						}
					});
					$(this).dialog('close').dialog('destroy');
				}
			},
			close: function(e, u) { $(this).dialog('destroy'); }
		});
	}

	function handleImgEditClick(target) {

		Shared.UploadManager.CancelAllUploads();

		var collectionBox = target.parents(".collectionBox");
		var imgContainer = collectionBox.find(".imgContainer");
		var targetImageId = imgContainer.find("img").attr("id");

		var uploadWindow = $("#uploadWindow");
		uploadWindow.attr("title", "Change the picture");
		uploadWindow.find("p").html("Select a JPEG image to upload.  This will serve as the cover art for the collection.  Maximum file size allowed is 3MB.");

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
			custom_settings: { target_image: targetImageId, target_div: "uploadWindow" },
			file_dialog_complete_handler: Shared.UploadManager.FileDialogComplete,
			file_queued_handler: Shared.UploadManager.FileQueued,
			file_queue_error_handler: Shared.UploadManager.FileQueueError,
			upload_start_handler: Shared.UploadManager.UploadStart,
			upload_progress_handler: Shared.UploadManager.UploadProgress,
			upload_error_handler: Shared.UploadManager.UploadError,
			upload_complete_handler: Shared.UploadManager.UploadComplete,
			upload_success_handler: imageUploadSucces,
			queue_complete_handler: imageQueueComplete,
			
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

	function imageQueueComplete(numFilesUploaded) {
		$("#uploadThrobber").hide();
	}

	function imageUploadSucces(file, serverData) {
		var img = $("#" + this.customSettings.target_image);
		
		//set src to temp image
		img.attr("src", Shared.Common.ProgressImageUrl);

		//recieve the url to the new image
		var response = Shared.Common.ParseJson(serverData);

		//set the img src to new url
		img.attr("src", response.ImageUrl);

		var collectionBox = img.parents(".collectionBox");
		var deleteImageLink = collectionBox.find(".deleteImage");
		deleteImageLink.removeClass("ui-state-disabled");

		Shared.Common.ShowUserMessage({ Type: 1, Message: "Picture was changed." });
		$("#uploadWindow").dialog('close').dialog('destroy');
		Shared.UploadManager.CancelAllUploads();
	}

	function handleImgDeleteClick(target) {
		var collectionBox = target.parents(".collectionBox");
		var imgContainer = collectionBox.find(".imgContainer");
		var deleteImageLink = collectionBox.find(".deleteImage");

		var img = imgContainer.find("img");
		var src = img.attr("src");
		var url = target.attr("href");
		var msg = 'Are you sure you want to remove the picture?';

		$("#modalWindow").attr("title", "Confirm").html(msg).dialog({
			resizable: true,
			height: 250,
			width: 350,
			modal: true,
			overlay: {
				backgroundColor: '#000',
				opacity: 0.5
			},
			buttons: {
				Cancel: function() {
					$(this).dialog('close').dialog('destroy');
				},
				'Yes, delete': function() {
					$.post(
		            url,
		            null,
		            function(data) {
						img.attr("src", Shared.Common.ProgressImageUrl);
						img.attr("src", data.ImageUrl);
		            	deleteImageLink.addClass("ui-state-disabled");
		            	Shared.Common.ShowUserMessage({ Type: 1, Message: "Picture was removed." });
		            },
		            "json"
		         );
					$(this).dialog('close').dialog('destroy');
				}
			},
			close: function(e, u) { $(this).dialog('destroy'); }
		});
	}
};