<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MusicCompany.Website.Models.AudioWorkDTO>" %>
<link href="/Content/swfupload/swfupload.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="/Content/swfupload/swfupload.js"></script>
<script type="text/javascript" src="/Content/swfupload/swfupload.swfobject.js"></script>
<script type="text/javascript" src="/Content/swfupload/forms/fileprogress.js"></script>
<script type="text/javascript" src="/Content/swfupload/forms/audioWorkHandlers.js"></script>

<script src="<%=Url.Content("~/Scripts/jquery.autocomplete.min.js") %>" type="text/javascript"></script>

<link href="<%=Url.Content("~/Content/jquery.autocomplete.css") %>" rel="stylesheet"
	type="text/css" />
	
<script type="text/javascript">
		var swfu;
		window.onload = function() {
			swfu = new SWFUpload({
				// Backend settings
				upload_url: "/Upload/SWFUpload", // Relative to the SWF file, you can use an absolute URL as well.
				file_post_name: "postedFile",

				post_params: { 
					"FileType": "AUDIO",
					"AUTHID" : "<%= Request.Cookies[FormsAuthentication.FormsCookieName].Value %>"
				},

				// Flash file settings
				file_size_limit: "64 MB",
				file_types: "*.wav;*.flac;*.mp3;*.ogg*", 		// or you could use something like: "*.doc;*.wpd;*.pdf",
				file_types_description: "Audio Files",
				file_upload_limit: "0",
				file_queue_limit: "1",

				// Event handler settings
				swfupload_loaded_handler: swfUploadLoaded,

				file_dialog_start_handler: fileDialogStart,
				file_queued_handler: fileQueued,
				file_queue_error_handler: fileQueueError,
				file_dialog_complete_handler: fileDialogComplete,

				//upload_start_handler : uploadStart,	// I could do some client/JavaScript validation here, but I don't need to.
				upload_progress_handler: uploadProgress,
				upload_error_handler: uploadError,
				upload_success_handler: uploadSuccess,
				upload_complete_handler: uploadComplete,

				// Button Settings
				button_image_url: "/Content/XPButtonUploadText_61x22.png", // Relative to the SWF file
				button_placeholder_id: "spanButtonPlaceholder",
				button_width: 61,
				button_height: 22,

				// Flash Settings
				flash_url: "/Content/swfupload/swfupload.swf",

				custom_settings: {
					progress_target: "fsUploadProgress",
					upload_successful: false
				},
				
					// SWFObject settings
				minimum_flash_version : "9.0.28",
				swfupload_pre_load_handler : swfUploadPreLoad,
				swfupload_load_failed_handler : swfUploadLoadFailed,

				// Debug settings
				debug: false
			});
		};

		$(document).ready(function() {

			var url = '<%=Url.Action("SuggestTags") %>';
			$("#txtTags").autocomplete(url, {

				delay: 40,
				selectFirst: false,
				multiple: true,
				highlight: false

			});
		});


	});
</script>