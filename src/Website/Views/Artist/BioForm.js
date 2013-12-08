if (!window.Artist) { window.Artist = {}; }
Artist.BioForm = new function() {
	var _my = this;
	_my.HijaxForm = function(form) {
		form.find("textarea:first").focus();
	};
}