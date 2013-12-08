if (!window.Comments) { window.Comments = {}; }
Comments.CommentList = new function() {
	var _my = this;

	_my.HijaxComments = function() {
		//hijax the add link
		$(".addComment").click(function(e) {
			e.preventDefault();
			var target = $(e.target);
			var addBox = target.parents(".addBox");
			addBox.hide();
			var commBox = target.parents(".commBox");
			handleAddCommentClick(target, commBox);
		});


		$(".pageLink").click(function(e) {
			e.preventDefault();
			var clicked = $(e.target);
			var url;
			if (clicked.is("a")) {
				url = clicked.attr("href"); ;
			} else {
				url = clicked.parent("a").attr("href");
			}
			var commBox = clicked.parents(".commBox");
			clicked.html("loading...");
			clicked.addClass("ui-state-disabled");
			$.get(url, function(data) {
				commBox.html(data);
				_my.HijaxComments();
			});
		});

	}

	function handleAddCommentClick(target, commBox) {
		var formBox = commBox.find(".formBox");
		if (Shared.Common.IsEmpty(formBox.html())) {
			formBox.html(Shared.Common.ProgressImage);
			var url = $(target).attr("href");
			var form = $.get(url, function(data) {
				var formBox = target.parent().next(".formBox");
				formBox.html(data);
				var form = formBox.find("form");
				//TODO: would be nice to delay load this, someday....
				//$.getScript("/Views/Comments/CommentForm.js", function() {
				Comments.CommentForm.HijaxForm(form);
				//});
				var cancel = form.find("a.cancel");
				cancel.click(function(e) {
					e.preventDefault();
					formBox.html("");
					var addBox = formBox.parent().find(".addBox");
					addBox.show();
				});
				form.submit(function() {
					handleCommentFormSubmit(form, commBox);
					return false;
				});
			});
		}
	}

	function handleCommentFormSubmit(form, commBox) {
		if (form.valid()) {
			var url = form.attr("action");
			$.ajax({
				type: "POST",
				url: url,
				data: form.serialize(),
				dataType: "json",
				success: function(data) {
					commBox.html(Shared.Common.ProgressImage);
					Shared.Common.ShowUserMessage(data);
					var expander = commBox.prev(".expandComm");
					var refreshUrl = expander.attr("href");
					$.ajax({
						type: "GET",
						url: refreshUrl,
						success: function(newData) {
							commBox.html(newData);
							_my.HijaxComments();
							if (data.Message.indexOf("Welcome", 0) != -1) {
								var addLink = commBox.find(".addComment");
								addLink.click();
							} else {
								var newComment = commBox.find(".comment:first");
								newComment.addClass("ui-state-highlight");
							}

						}
					});
				},
				error: function(XMLHttpRequest, textStatus, errorThrown) {
					commBox.html(XMLHttpRequest.responseText);
				}
			});
		}
	}
}