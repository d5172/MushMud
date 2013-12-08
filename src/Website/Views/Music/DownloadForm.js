if (!window.Music) { window.Music = {}; }

Music.DownloadForm = new function() {
    var _my = this;

    _my.Ready = function() {
        Shared.Common.HijaxButtonHoverStates();
        $(".confirmDownloadButton").click(function(e) {
            $("#downloadWindow").dialog("close");
        });
    }
}