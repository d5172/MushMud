if (!window.Shared) { window.Shared = {}; }

Shared.LogOnForm = new function() {
	var _my = this;

	_my.Ready = function() {
		$("#username").focus();
	};
};

//jQuery Ready function
$(function() {
	Shared.LogOnForm.Ready();
});