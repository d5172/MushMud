<%@ CONTROL language="C#" inherits="System.Web.Mvc.ViewUserControl<MusicCompany.Common.ViewModel.AudioSingleSummaryView>" %>
<div class="playerContainer">
	<div style="float: left">
		<a href="#Play" class="playSingleButton playerButton stopped fg-button ui-state-default fg-button-icon-left ui-corner-all" style="width: 3em" title="Play"><span class="ui-icon ui-icon-play"></span><span>Play</span></a>
		<div class="ui-helper-hidden trackList" id="<%=Model.Identifier %>">
			<div class="trackRow">
				<a href="<%=Url.Action("PlaySingle", "Stream",new{artistId = Model.ArtistIdentifier, workId=Model.Identifier}) %>" class="playTrackButton">Play</a>
			</div>
		</div>
	</div>
	<div style="float: left" class="player ui-helper-hidden">
		<div style="float: left; padding-top: .4em">
			<%=Html.IconLinkButton("Stop", "ui-icon-stop", "stopButton", "#Stop") %>
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