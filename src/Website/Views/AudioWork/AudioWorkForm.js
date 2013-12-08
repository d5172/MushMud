if (!window.AudioWork) { window.AudioWork = {}; }

AudioWork.AudioWorkForm = new function() {
	var _my = this;

	_my.HijaxForm = function(form) {

		form.find("a.licenseHelp").click(function(e) {
			e.preventDefault();
			var help = $("#modalWindow");
			var url = $(e.target).attr("href");
			if (!url) { url = $(e.target).parent().attr("href"); }
			help.attr("title", "Available Licenses");
			help.dialog({
				resizable: true,
				height: 300,
				width: 450,
				buttons: {
					"Close": function() { help.dialog('destroy'); }
				},
				open: function(event, ui) {
					help.html(Shared.Common.ProgressImage);
					$.get(url, '', function(data) {
						help.html(data);
					}, "html")
				},
				close: function(event, ui) {
					help.dialog('destroy');
				}
			});
		});

		form.find(".suggestTags").autocomplete(Shared.Common.SuggestTagsUrl, {
			delay: 40,
			selectFirst: true,
			multiple: true,
			highlight: false
		});

		form.validate(
        {
        	errorClass: "ui-state-error",
        	errorElement: "div",
        	rules: {
        		"Title": { required: true, maxlength: 100 }
        	},
        	messages: {
        		"Title": { maxlength: "The Title is too long (up to 100 characters allowed)" }
        	}
        });

		form.find("input:first").focus();
	}
}