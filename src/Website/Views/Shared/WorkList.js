if (!window.Shared) { window.Shared = {}; }

Shared.WorkList = new function() {

	var _my = this;

	_my.HijaxImageLinks = function() {
		$(".imageLink").click(function(e) {
			e.preventDefault();
			var url = $(this).attr("href");
			var popup = $("#lightBox");
			var title = $(e.target).attr("title");
			if (Shared.Common.IsEmpty(title)) {
				title = "View Image";
			}
			popup.attr("title", title);
			popup.dialog({
				height: 450,
				width: 430,
				modal: true,
				overlay: {
					backgroundColor: '#000',
					opacity: 0.5
				},
				open: function(event, ui) {
					popup.html(Shared.Common.ProgressImage);
					$.ajax({
						type: "GET",
						url: url,
						dataType: "html",
						cache: false,
						success: function(data) {
							popup.html(data);
						},
						error: function(XMLHttpRequest, textStatus, errorThrown) {
							popup.html(XMLHttpRequest.responseText);
						}
					})
				},
				close: function(event, ui) {
					popup.dialog('destroy');
				}
			});
		});
	}

	_my.HijaxProfileLinks = function() {
		//expand profile link on click
		$(".expandProfile").click(function(e) {
			e.preventDefault();
			var profileContainer = $(this).next(".profileContainer");
			profileContainer.toggle("blind", null, 200);
			if (Shared.Common.IsEmpty(profileContainer.html())) {
				profileContainer.html(Shared.Common.ProgressImage);
				var url = $(this).attr("href")
				$.ajax({
					type: "GET",
					url: url,
					dataType: "html",
					cache: false,
					success: function(data) {
						profileContainer.html(data);
						_my.HijaxImageLinks();
					},
					error: function(XMLHttpRequest, textStatus, errorThrown) {
						profileContainer.html(XMLHttpRequest.responseText);
					}
				});
			}
		});
	}

	_my.HijaxCommentLinks = function() {
		//expand comments link on click
		$(".expandComm").click(function(e) {
			e.preventDefault();
			var link = $(this);
			var commentsContainer = link.next(".commBox");
			commentsContainer.toggle("blind", null, 200);
			if (Shared.Common.IsEmpty(commentsContainer.html())) {
				commentsContainer.html(Shared.Common.ProgressImage);
				var url = link.attr("href")
				$.ajax({
					type: "GET",
					url: url,
					dataType: "html",
					cache: false,
					success: function(data) {
						commentsContainer.html(data);
						Comments.CommentList.HijaxComments();
					},
					error: function(XMLHttpRequest, textStatus, errorThrown) {
						commentsContainer.html(XMLHttpRequest.responseText);
					}
				});
			}
		});
	}

	_my.HijaxDescriptionLinks = function() {
		//expand profile link on click
		$(".expandDescription").click(function(e) {
			e.preventDefault();
			var link = $(this);
			var descriptionContainer = link.parent().find(".descriptionContainer");
			var descriptionTuncated = link.prev(".descriptionTruncated");
			descriptionTuncated.toggle("blind", null, 200);
			descriptionContainer.toggle("blind", null, 200);
			if (Shared.Common.IsEmpty(descriptionContainer.html())) {
				descriptionContainer.html(Shared.Common.ProgressImage);
				var url = link.attr("href")
				$.ajax({
					type: "GET",
					url: url,
					dataType: "html",
					cache: false,
					success: function(data) {
						descriptionContainer.html(data);
					},
					error: function(XMLHttpRequest, textStatus, errorThrown) {
						descriptionContainer.html(XMLHttpRequest.responseText);
					}
				});
			}
			var defaultText = "Read More";
			if (link.html() == defaultText) {
				link.html("Close");
			} else {
				link.html(defaultText);
			}
		});
	}

	_my.HijaxDownloadLinks = function() {

		$(".downloadButton").click(function(e) {

			e.preventDefault();
			var url = $(this).attr("href");

			var popup = $("#downloadWindow");

			popup.dialog({
				height: 400,
				width: 600,
				modal: true,
				overlay: {
					backgroundColor: '#000',
					opacity: 0.5
				},
				buttons: {
					"Cancel": function() { popup.dialog('destroy'); }
				},
				open: function(event, ui) {
					popup.html(Shared.Common.ProgressImage);
					$.ajax({
						type: "GET",
						url: url,
						dataType: "html",
						cache: false,
						success: function(data) {
							popup.html(data);
							Music.DownloadForm.Ready();
						},
						error: function(XMLHttpRequest, textStatus, errorThrown) {
							popup.html(XMLHttpRequest.responseText);
						}
					})
				},
				close: function(event, ui) {
					popup.dialog('destroy');
				}
			});

		});
	}

}