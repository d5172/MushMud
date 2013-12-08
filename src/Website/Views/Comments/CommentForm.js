if (!window.Comments) { window.Comments = {}; }
Comments.CommentForm = new function() {
	var _my = this;

	_my.HijaxForm = function(form) {

		$.getScript("/Scripts/jquery.validate.min.js", function() {
			$(form).validate(
			{
				errorClass: "ui-state-error",
				errorElement: "div",
				rules: {
					"CommentText": { required: true, maxlength: 1000 }
				}
			});
		});

		form.find("textarea:first").focus();
	}
}