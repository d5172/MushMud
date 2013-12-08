<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<MusicCompany.Common.ViewModel.AudioTrackSummaryView>>" %>
<%foreach (var track in Model)
  { %>
<div class="audioWorkContainer" style="padding: .5em 0 .5em 0; height: 1.5em; margin-left: 0px;" id="<%= track.Identifier.ToString() %>">
	<div class="audioWorkToolContainer" style="float: left; min-width: 20px; margin-left: .4em; position: absolute;">
		<div class="ui-helper-hidden sortButton">
			<span class="ui-icon ui-icon-arrow-2-n-s" style="cursor: move" title="Sort"></span>
		</div>
	</div>
	<div class="audioWorkDetailContainer" style="float: left; min-width: 400px; margin-left: 1.75em; padding-top: .5em; position: absolute;">
		<div style="float: left" class="optionsBox">
			<div>
				<%=Html.IconLinkButton("Options", "ui-icon-triangle-1-s", "optionsLink", "#Options") %>
				<div class="ui-helper-clearfix">
				</div>
			</div>
			<div class="ui-helper-hidden ui-corner-tr ui-corner-bottom optionsListBox" style="background-color: #fff; border: solid 1px silver; position: absolute; padding: .5em .5em .5em .5em; z-index: 100">
				<%=Html.IconLink("Edit", "Edit the song", "ui-icon-pencil", "editSong", Url.Action("EditTrack", "AudioWork", new{artistId = track.ArtistIdentifier,workId=track.Identifier,collectionId=track.CollectionIdentifier})) %>
				<br />
				<%=Html.IconLink("Play", "Play the song", "ui-icon-play", "playSong", Url.Action("PlayTrack", "Stream",new{artistId = track.ArtistIdentifier,workId=track.Identifier,collectionId=track.CollectionIdentifier}))  %>
				<br />
				<%=Html.IconLink("Download", "Download song", "ui-icon-arrow-1-s", "downloadSong", Url.Action("DownloadTrack", "Music", new{artistId = track.ArtistIdentifier,workId=track.Identifier,collectionId=track.CollectionIdentifier})) %>
				<br />
				<%=Html.IconLink("Remove", "Remove the song from the collection", "ui-icon-close", "removeSong", Url.Action("RemoveFromParent", "Works", new{artistId = track.ArtistIdentifier,workId=track.Identifier,collectionId=track.CollectionIdentifier})) %>
			</div>
		</div>
		<div class="trackNumber" title="sort" style="float: left; width: 1em; padding: 0 .75em 0 .5em; font-weight: bold;">
			<%=track.ViewOrder+ 1 %>.
		</div>
		<div class="songTitle" style="float: left; width: 24em; padding: 0 .75em 0 .5em;">
			<%=Html.Encode(track.Title) %>
				<%--<br />
				<%=Html.Encode(work.Description) %>
				<br />
				<%=Html.DisplayTags(work, "tags: ") %>--%>
			
		</div>
		<div style="float: left; padding: 0 .75em 0 .5em;">
			<%=Html.DisplayShortDuration(TimeSpan.FromSeconds(track.Seconds)) %>
		</div>
		<div style="float: left; padding: 0 .75em 0 .5em;">
			<%=Html.DisplayFileFormats(track.FileFormat, track.AlternateFileFormat) %>
		</div>
	</div>
	<div class="ui-helper-clearfix">
	</div>
</div>
<div class="audioWorkEditContainer ui-helper-hidden ui-corner-all" style="background-color: #FFF; padding: 4px 0 4px 0; margin: 1em 0 0 1em; max-width: 475px;">
	<div class="trackNumber" title="sort" style="float: left; width: 1em; padding: 0 .75em 0 .5em; font-weight: bold;">
		<%=track.ViewOrder + 1 %>.
	</div>
	<div class="audioWorkEditBox" style="float: left">
	</div>
	<div class="ui-helper-clearfix">
	</div>
</div>
<%} %>