if (!window.Shared) { window.Shared = {}; }

Shared.OptionsLink = new function() {
	var _my = this;

	_my.HijaxOptionsLinks = function() {
		$(document).unbind("click");
		$(document).bind("click", function(e) {
			var target = $(e.target);
			if (target.parents(".optionsBox").length > 0) {
				e.preventDefault();
				toggleOptionsLink(target.parents(".optionsLink"));
			} else {
				hideAllOptionsLinks(null);
			}

		});

	};

	function hideAllOptionsLinks(exceptOne) {
		var links;
		if (exceptOne) {
			links = $(".optionsLink").not(exceptOne);
		} else {
			links = $(".optionsLink");
		}
		links.each(function(i) {
			hideOptionsLink($(this));
		});
	}

	function hideOptionsLink(optionsLink) {
		var icon = optionsLink.find("span.ui-icon");
		if (icon.hasClass("ui-icon-triangle-1-n")) {
			optionsLink.addClass("ui-corner-all").removeClass("ui-corner-top");
			icon.removeClass("ui-icon-triangle-1-n").addClass("ui-icon-triangle-1-s");
			var optionsListBox = optionsLink.parents(".optionsBox").find(".optionsListBox");
			optionsListBox.hide();
		}
	}

	function toggleOptionsLink(optionsLink) {
		optionsLink.toggleClass("ui-corner-all").toggleClass("ui-corner-top");
		var icon = optionsLink.find("span.ui-icon");
		icon.toggleClass("ui-icon-triangle-1-s").toggleClass("ui-icon-triangle-1-n");
		var optionsListBox = optionsLink.parents(".optionsBox").find(".optionsListBox");
		optionsListBox.toggle("blind", null, 50);
		hideAllOptionsLinks(optionsLink);
	}
};