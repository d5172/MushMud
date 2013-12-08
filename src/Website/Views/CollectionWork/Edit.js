if (!window.CollectionWork) { window.CollectionWork = {}; }

CollectionWork.Edit = new function() {
    var _my = this;

    _my.HijaxAll = function() {
        var form = $(".collectionWorkForm");
        if (!form) { alert("no form"); }
        CollectionWork.CollectionWorkForm.HijaxForm(form);
    };
};

//JQuery Ready
$(function() {
    CollectionWork.Edit.HijaxAll();
});

