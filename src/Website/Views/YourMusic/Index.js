if (!window.YourMusic) { window.YourMusic = {}; }

YourMusic.Index = new function() {
	var _my = this;

	_my.ShowRecentActivity = function(url) {
		var box = $("#activityBox");
		box.html(Shared.Common.ProgressImage);
		if (!url) {
			url = global_WorkActivityUrl;
		}
		$.ajax({
			type: "GET",
			url: url,
			//cache: false,
			dataType: "html",
			success: function(data) {
				box.html(data);
				$(".pageLink").click(function(e) {
					e.preventDefault();
					var clicked = $(e.target);
					var url;
					if (clicked.is("a")) {
						url = clicked.attr("href"); ;
					} else {
						url = clicked.parent("a").attr("href");
					}
					YourMusic.Index.ShowRecentActivity(url);
				});
			},
			error: function(XMLHttpRequest, textStatus, errorThrown) {
				box.html(XMLHttpRequest.responseText);
			}
		});
	};
};

//JQuery Ready
$(function() {
	YourMusic.Index.ShowRecentActivity();
});