if (!window.Shared) { window.Shared = {}; }

Shared.Common = new function() {

	var _my = this;

	//ideally, we would generate these urls dynamically using Url.Route(...
	_my.ProgressImageUrl = "/content/progress.gif";
	_my.ProgressImage = '<img src="' + _my.ProgressImageUrl + '" />';
	_my.SuggestTagsUrl = '/Works/SuggestTags';

	_my.InitialUserMessage = null;

	/// To be called when jQuery is ready
	_my.Ready = function() {

		//search box auto complete
		$("#txtSearch").autocomplete("/Music/AutoCompleteSearchTerm/", {
			delay: 40,
			selectFirst: true,
			multiple: true,
			highlight: false
		});

		//search button click
		$("#lnkSearch").click(function(e) {
			e.preventDefault();
			var txtSearch = $("#txtSearch");
			var input = txtSearch.val();
			if (input != '') {
				var theForm = $(this).parents("form");
				theForm.submit();
			}
		});

		//highlight the active tab
		$(".navTab").each(function(i) {
			var tab = $(this);
			var link = tab.find("a");
			var href = link.attr("href");
			if (document.location.href.indexOf(href) != -1) {
				tab.addClass("ui-state-active");
				return false;
			}
		});

		//dismiss the usermessage with a click
		$("#userMessage").click(function(e) {
			$("#userMessage").fadeOut(100);
		});

		//show a default usermessage is exists
		if (_my.InitialUserMessage != null) {
			_my.ShowUserMessage(_my.InitialUserMessage);
		}
	}

	/// wires up all the fg button hover effects
	_my.HijaxButtonHoverStates = function() {
		$(".fg-button:not(.ui-state-disabled)").hover(
            function() { $(this).addClass("ui-state-hover"); },
            function() { $(this).removeClass("ui-state-hover"); }
        );
	}

	/// Shows a single user message
	_my.ShowUserMessage = function(userMessage) {
		var box = $("#userMessage");
		box
        .toggleClass("ui-state-error", userMessage.Type == 0)
        .toggleClass("ui-state-highlight", userMessage.Type == 1)
        .toggleClass("Warning", userMessage.Type == 2)
        .html(userMessage.Message)
        .show();
		if (userMessage.Type != 0) {
			box.delay(2500).fadeOut(100);
		}
	}

	///shows an array of user messages
	_my.ShowUserMessages = function(userMessages) {
		var box = $("#userMessage");
		box.toggleClass("ui-state-highlight");
		box.empty();
		var fadeOut = true;
		$.each(userMessages, function(i, val) {
			box.append($("<div></div>"));
			var msgNode = box.find("div:last");
			msgNode.toggleClass("Error", val.Type == 0);
			msgNode.toggleClass("Info", val.Type == 1);
			msgNode.toggleClass("Warning", val.Type == 2);
			msgNode.html(val.Message);
			if (val.Type == 0) {
				fadeOut = false;
			}
		});
		box.show();

		if (fadeOut) {
			box.delay(2500).fadeOut(100);
		}
	}

	/// string is null or empty
	_my.IsEmpty = function(str) {
		return str.replace(/\s/g, "") == "";
	}

	/// shows a file size in a friendly string
	_my.FileSize = function(bytes) {
		var s = ['bytes', 'kb', 'MB', 'GB', 'TB', 'PB'];
		var e = Math.floor(Math.log(bytes) / Math.log(1024));
		return (bytes / Math.pow(1024, Math.floor(e))).toFixed(2) + " " + s[e];
	}

	/// parses a string into a json object
	///  TODO: use a library to make this safer
	_my.ParseJson = function(jsonString) {
		return eval("(" + jsonString + ")");
	}
}

//jQuery Ready function
$(function() {
    Shared.Common.Ready();
    Shared.Common.HijaxButtonHoverStates();
});