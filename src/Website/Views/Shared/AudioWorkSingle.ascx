<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MusicCompany.Common.ViewModel.AudioSingleSummaryView>" %>
<div class="audioWorkContainer" style="padding: .5em 0 .5em 0; height: 1.5em; margin-left: 0px;" id="<%= Model.Identifier.ToString() %>">
	<div class="audioWorkDetailContainer" style="min-width: 400px; margin-left: 1.75em; padding-top: .5em;">
		<div style="float: left" class="optionsBox">
			<div>
				<%=Html.IconLinkButton("Options", "ui-icon-triangle-1-s", "optionsLink", "#Options") %>
				<div class="ui-helper-clearfix">
				</div>
			</div>
			<div class="ui-helper-hidden ui-corner-tr ui-corner-bottom optionsListBox" style="background-color: #fff; border: solid 1px silver; position: absolute; padding: .5em .5em .5em .5em; z-index: 100">
				<%=Html.IconLink("Edit", "Edit the song", "ui-icon-pencil", "editSingle", Url.Action("EditSingle", "AudioWork", new{artistId = Model.ArtistIdentifier,workId=Model.Identifier})) %>
				<br />
				<%=Html.IconLink("Play", "Play the song", "ui-icon-play", "playSingle", Url.Action("PlayTrack", "Stream",new{artistId = Model.ArtistIdentifier,workId=Model.Identifier}))  %>
				<br />
				<%=Html.IconLink("Download", "Download song", "ui-icon-arrow-1-s", "downloadSingle", Url.Action("DownloadSingle", "Music", new{artistId = Model.ArtistIdentifier,workId=Model.Identifier})) %>
				<br />
				<%=Html.IconLink("Add to Collection", "Add to Collection", "ui-icon-arrowreturnthick-1-n", "addToCollection", Url.Action("AddToCollection", "AudioWork", new{artistId = Model.ArtistIdentifier,workId=Model.Identifier})) %>
				<br />
				<%=Html.IconLink("Delete", "Delete the song", "ui-icon-trash", "deleteSingle", Url.Action("Delete", "Works", new{artistId = Model.ArtistIdentifier,workId=Model.Identifier})) %>
			</div>
		</div>
		<div class="songTitle" style="float: left; width: 24em; padding: 0 .75em 0 .5em;">
			<%=Html.Encode(Model.Title) %>
		</div>
		<div style="float: left; padding: 0 .75em 0 .5em;">
			<%=Html.DisplayShortDuration(TimeSpan.FromSeconds(Model.Seconds)) %>
		</div>
		<div style="float: left; padding: 0 .75em 0 .5em;">
			<%=Html.DisplayFileFormats(Model.FileFormat, Model.AlternateFileFormat) %>
		</div>
		<div style="float: left; padding: 0 .75em 0 .5em;">
			<%=Html.Encode(Model.License.Name) %>
		</div>
		<div style="float: left; padding: 0 .75em 0 .5em;">
			<%=Model.ReleaseDate.ToShortDateString()%>
		</div>
		<div style="float: left; padding: 0 .75em 0 .5em;">
			<%=Html.DisplayTags(Model.Tags, "tags: ") %>
		</div>
		<div class="ui-helper-clearfix">
		</div>
	</div>
</div>
<div class="audioWorkEditContainer ui-helper-hidden ui-corner-all" style="background-color: #FFF; padding: 4px 0 4px 0; margin: 1em 0 0 1em; max-width: 475px;">
	<div class="audioWorkEditBox">
	</div>
</div>
