if (!window.Artist) { window.Artist = {}; }


Artist.ArtistForm = new function() {

	var _my = this;
	
	_my.HijaxForm = function(form) {
		form.validate(
        {
        	errorClass: "ui-state-error",
        	errorElement: "div",
        	rules: {
        	"ArtistName": { required: true, maxlength: 100, remote: { url: global_NameAvailableUrl, type: "post", data: { artistId: global_ExistingArtistId}} }
        	},
        	messages: {
        		"ArtistName": { remote: "That name is already taken" }
        	}
        });
	
	};
}