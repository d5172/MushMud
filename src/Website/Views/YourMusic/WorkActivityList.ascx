<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MusicCompany.Website.Models.YourMusic.WorkActivityViewModel>" %>
<div>
	<%foreach ( var item in Model.Events )
   { %>
	<div style="margin-bottom: .5em">
		<%=Html.DisplayWho(item) %>
		<%=Html.DisplayWhat(item) %>
		<%if ( item.WorkType == "Collection" )
	{ %>
		<%=Html.ActionLink(item.Title, "Collection", "Music", new {artistId = item.ArtistIdentifier, workId = item.WorkIdentifier}, null) %>
		<%}
	else if ( string.IsNullOrEmpty(item.CollectionIdentifier) )
	{ %>
		<%=Html.ActionLink(item.Title, "Single", "Music", new {artistId = item.ArtistIdentifier, workId = item.WorkIdentifier}, null) %>
		<%}
	else
	{ %>
		<%=Html.ActionLink(item.Title, "Collection", "Music", new {artistId = item.ArtistIdentifier, workId = item.CollectionIdentifier}, null) %>
		<%} %>
		by
		<%=Html.ActionLink(item.ArtistName, "Profile", "Artist", new {artistId = item.ArtistIdentifier}, null) %>
		<%=Html.DisplayWhen(item) %>
	</div>
	<%} %>
</div>
<div>
	<div style="float: left;">
		<a href="<%= Model.Events.HasPreviousPage ? Url.Action("WorkActivity", new {page=Model.Events.PageNumber-1}) : "" %>" class="pageLink <%=Model.Events.HasPreviousPage ? "" : "ui-state-disabled ui-helper-hidden" %>" title="Previous Page"><span class="ui-icon ui-icon-seek-prev" style="float: left; margin-right: .1em"></span>Newer</a>
	</div>
	<div style="float: right;">
		<a href="<%=Model.Events.HasNextPage ? Url.Action("WorkActivity", new {page=Model.Events.PageNumber+1}) : "" %>" class="pageLink <%=Model.Events.HasNextPage ? "" : "ui-state-disabled ui-helper-hidden" %>" title="Next Page"><span class="ui-icon ui-icon-seek-next" style="float: right; margin-left: .1em"></span>Older</a>
	</div>
	<div class="ui-helper-clearfix">
	</div>
</div>
