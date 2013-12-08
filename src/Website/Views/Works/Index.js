if (!window.Works) { window.Works = {}; }

Works.Index = new function() {
	var _my = this;

	_my.HijaxAll = function() {
		Shared.OptionsLink.HijaxOptionsLinks();
		Works.CollectionWorkList.HijaxCollections();
		Shared.AudioWorkList.HijaxLists();
		Shared.AudioWorkSingleList.HijaxSingles();
		// Shared.PlayerManager.SetupPlayers();

	};

	_my.RefreshCollectionWorks = function() {
		refreshContainer($("#collectionWorks"), global_collectionWorksUrl);
	};

	_my.RefreshSingleWorks = function() {
		var targetDiv = $("#singleWorks");
		var parentBox = targetDiv.parents("#singlesBox");
		$.ajax({
			type: "GET",
			url: global_singleWorksUrl,
			dataType: "html",
			cache: false,
			success: function(data) {
				targetDiv.html(data);
				if (data == '') {
					parentBox.hide();
				} else {
					parentBox.show();
				}
				Works.Index.HijaxAll();
			},
			error: function(XMLHttpRequest, textStatus, errorThrown) {
				targetDiv.html(XMLHttpRequest.responseText);
			}
		});
	};

	_my.HandleDownloadClick = function(target) {
		var url = $(target).attr("href");
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
	}

	function refreshContainer(targetDiv, sourceUrl) {
		$.ajax({
			type: "GET",
			url: sourceUrl,
			dataType: "html",
			cache: false,
			success: function(data) {
				targetDiv.html(data);
				Works.Index.HijaxAll();
			},
			error: function(XMLHttpRequest, textStatus, errorThrown) {
				targetDiv.html(XMLHttpRequest.responseText);
			}
		});
	}
}

//JQuery Ready
$(function() {
    Works.Index.HijaxAll();
});

