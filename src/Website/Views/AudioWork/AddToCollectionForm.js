if (!window.AudioWork) { window.AudioWork = {}; }

AudioWork.AddToCollectionForm = new function() {
	var _my = this;

	_my.HijaxForm = function(form) {

		form.find("#ddlCollection").change(function(e) {
			if (form.valid()) {
				form.submit();
			}
		});

		form.validate(
        {
        	errorClass: "ui-state-error",
        	errorElement: "div",
        	rules: {
        		"CollectionIdentifier": { required: true }
        	},
        	messages: {
        		"CollectionIdentifier": { maxlength: "Please choose a collection" }
        	}
        });

	}
}