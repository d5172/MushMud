<%@ CONTROL language="C#" inherits="System.Web.Mvc.ViewUserControl<MusicCompany.Common.ViewModel.CollectionSummaryView>" %>
<div class="playerContainer">
	
	<div style="float: left">
		<a href="#Play" class="playCollectionButton playerButton stopped fg-button ui-state-default fg-button-icon-left ui-corner-all" style="width: 3em" title="Play all tracks"><span class="ui-icon ui-icon-play"></span><span>Play</span></a>
	</div>
	
	<div style="float: left" class="player ui-helper-hidden">
		<div style="float: left; padding-top: .4em">
			<%=Html.IconLinkButton("Previous", "ui-icon-seek-prev", "prevButton", "#Previous") %>
		</div>
		<div style="float: left; padding-top: .4em">
			<%=Html.IconLinkButton("Stop", "ui-icon-stop", "stopButton", "#Stop") %>
		</div>
		<div style="float: left; padding-top: .4em">
			<%=Html.IconLinkButton("Next", "ui-icon-seek-next", "nextButton", "#Next") %>
		</div>
		<div style="float: left; padding-top: .4em">
			<%=Html.IconLinkButton("Repeat", "ui-icon-refresh", "repeatButton", "#Repeat") %>
		</div>
		<div style="float: left">
			<div class="playProgress" style="margin: .2em 0 .1em 0; height: .7em; width: 11em">
			</div>
			<div class="loadProgress" style="height: .4em; width: 11em">
			</div>
		</div>
		<div style="float: left; margin-left: 1em; font-size: .8em">
			<span class="playTime"></span>/ <span class="totalTime"></span>
		</div>
		<div class="ui-helper-clearfix">
		</div>
	</div>
	<div class="ui-helper-clearfix">
	</div>
</div>
<div class="expandContainer" style="margin-top: 5px">
	<div style="float: left; margin-top: 5px;">
		<a href="<%=Url.Action("ListTracks","Music",new{artistId = Model.ArtistIdentifier,collectionId=Model.Identifier})%>" title="Show Tracks" class="expandTrackList ui-icon ui-icon-triangle-1-e" rel="nofollow"></a>
	</div>
	<div style="float: left; margin-top: 6px;">
		<%= Html.DisplayCount("track", Model.TrackCount) %>&nbsp;(<%= Html.DisplayDuration(Model.Seconds)%>)
	</div>
	<div class="ui-helper-clearfix">
	</div>
	<div style="margin-top: .5em; " class="trackList ui-helper-hidden expandable" id="<%=Model.Identifier %>">
	</div>
</div>
