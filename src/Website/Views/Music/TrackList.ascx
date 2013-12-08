<%@ CONTROL language="C#" inherits="System.Web.Mvc.ViewUserControl<MusicCompany.Website.Models.Music.ListTracksViewModel>" %>
<div class="ui-corner-all" style="background-color: #e8eef4;">
	<table>
		<%foreach (AudioTrackSummaryView track in Model.Tracks)
	{ %>
		<tr class="trackRow">
			<td valign="top">
				<%=Html.IconLinkButton("Play", "ui-icon-play", "playTrackButton stopped", Url.Action("PlayTrack", "Stream",new{artistId = track.ArtistIdentifier,workId=track.Identifier,collectionId=track.CollectionIdentifier}))  %>
			</td>
			<td valign="top">
				<b>
					<%=track.ViewOrder + 1%>.</b>
			</td>
			<td width="100%" valign="top">
				<%=Html.ActionLink(track.Title, "Track", "Music", new{artistId = track.ArtistIdentifier,workId=track.Identifier,collectionId=track.CollectionIdentifier}, new {@class="audioWorkTitle", title=Html.DisplayTags(track.Tags, "tags: ")})  %>
				<div class="ui-helper-hidden audioWorkDetailContainer">
				</div>
			</td>
			<td valign="top">
				<%=Html.IconLinkButton("Download File", "ui-icon-arrow-1-s ui-priority-secondary", "downloadButton", Url.Action("DownloadTrack", "Music", new{artistId = track.ArtistIdentifier,workId=track.Identifier,collectionId=track.CollectionIdentifier})) %>
			</td>
		</tr>
		<%} %>
	</table>
</div>