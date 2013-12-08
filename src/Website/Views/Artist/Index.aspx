<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MusicCompany.Website.Models.Artist.IndexViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Html.FormatTitle("List of Artists") %>
</asp:Content>
<asp:Content ContentPlaceHolderID="AdditionalScripts" runat="server">
	<%=Html.ScriptFromManifest("Artist.Index")%>
</asp:Content>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
	<meta name="ROBOTS" content="INDEX, FOLLOW" />
	<meta name="description" content="List of the artists sharing creative commons licensed music" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h1>
		Artists</h1>
	<div style="padding-top: 1.5em">
		<% foreach ( var artist in Model.Artists )
	 { %>
		<div style="float: left; margin: 0 3em 1.5em 0; height: 14em; max-width: 125px">
			<div >
				<a href="<%=Url.Action("Profile", "Artist", new {artistId = artist.Identifier}) %>">
					<img src="<%=Url.Action("Artist", "Image", new {id = artist.ProfileImageId, width = 115})  %>" width="115" style="width: 115; border: none" alt="Artist Picture: <%= Html.Encode(artist.Name) %>" title="<%= Html.Encode(artist.Name) %>" />
				</a>
				<div style="text-align: center;">
					<%= Html.ActionLink(artist.Name, "Profile", "Artist", new{artistId = artist.Identifier}, new {title="View Artist Profile"})%>
				</div>
			</div>
		</div>
		<% } %>
		<div class="ui-helper-clearfix">
		</div>
	</div>
	<%if ( Model.Artists.HasNextPage )
   { %>
	<div>
		<div style="font-weight: bold; margin-bottom: .5em">
			Artists
			<%=Model.Artists.StartItem()%>-<%=Model.Artists.EndItem()%>
			of
			<%=Model.Artists.TotalItemCount%>
		</div>
		<div>
			<div class="pageLink ui-corner-all ui-state-default" style="float: left; margin-right: .5em; padding: .2em .8em .2em .8em">
				<a href="<%=Url.Action("Index", new {page=1}) %>" class="<%=Model.Artists.IsFirstPage ? "ui-state-disabled" : "" %>" title="First Page"><span class="ui-icon ui-icon-seek-first" style="float: right; margin-left: .1em"></span>First</a>
			</div>
			<div class="pageLink  ui-corner-all ui-state-default" style="float: left; margin-right: .5em; padding: .2em .8em .2em .8em">
				<a href="<%= Model.Artists.HasPreviousPage ? Url.Action("Index", new {page=Model.Artists.PageNumber-1}) : "" %>" class="<%=Model.Artists.HasPreviousPage ? "" : "ui-state-disabled" %>" title="Previous Page"><span class="ui-icon ui-icon-seek-prev" style="float: right; margin-left: .1em"></span>Prev</a>
			</div>
			<div class="pageLink  ui-corner-all ui-state-default" style="float: left; margin-right: .5em; padding: .2em .8em .2em .8em">
				<a href="<%=Model.Artists.HasNextPage ? Url.Action("Index", new {page=Model.Artists.PageNumber+1}) : "" %>" class="<%=Model.Artists.HasNextPage ? "" : "ui-state-disabled" %>" title="Next Page"><span class="ui-icon ui-icon-seek-next" style="float: left; margin-right: .1em"></span>Next</a>
			</div>
			<div class="pageLink  ui-corner-all ui-state-default" style="float: left; padding: .2em .8em .2em .8em">
				<a href="<%=Url.Action("Index", new {page=Model.Artists.PageCount}) %>" class="<%=Model.Artists.IsLastPage ? "ui-state-disabled" : "" %>" title="Last Page"><span class="ui-icon ui-icon-seek-end" style="float: left; margin-right: .1em"></span>Last</a>
			</div>
			<div class="ui-helper-clearfix">
			</div>
		</div>
	</div>
	<%} %>
</asp:Content>
