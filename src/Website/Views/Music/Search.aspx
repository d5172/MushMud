<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MusicCompany.Website.Models.Music.SearchViewModel>" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
<meta name="ROBOTS" content="INDEX, FOLLOW" />
<meta name="description" content="Search results based on keywords, tags, titles and artist names" />
</asp:Content>
<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	<%=Html.FormatTitle("Search Results") %>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="AdditionalStylesheets">
	<style type="text/css">
		.workListItemBoxWidth
		{
			width: 450px;
		}
	</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="AdditionalScripts">
	<%=Html.LibraryScript("jquery.ui") %>
	<%=Html.LibraryScript("jquery.ui") %>
	<%=Html.Script("jquery.jplayer.min.js") %>
	<%=Html.ScriptFromManifest("Music.Search") %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<%Html.RenderPartial("SearchPartial"); %>
	<h2>
		Search Results</h2>
	<%Html.RenderPartial("WorkList", Model.List); %>
	<% if ( Model.List.PageCount > 1 )	{ %>
	<div>
		<div style="font-weight: bold; margin-bottom: .5em">
			Results Page:
		</div>
		<div >
			<%if ( Model.List.HasPreviousPage )	 { %>
			<div class="pageLink  ui-corner-all ui-state-default" style="float: left; margin-right: .5em; padding: .2em .8em .2em .8em">
				<a href="<%= Model.List.HasPreviousPage ? Url.Action("Search", new {terms = Model.Terms, page=Model.List.PageNumber-1}) : "" %>" class="<%=Model.List.HasPreviousPage ? "" : "ui-state-disabled" %>" title="Previous Page"><span class="ui-icon ui-icon-seek-prev" style="float: right; margin-left: .1em"></span>Prev</a>
			</div>
			<%}%>
			<div style="float: left; margin-right: .5em; padding: .2em .8em .2em .8em">
				<%=Html.PageLinks(Model.List.PageNumber, Model.List.PageCount, "", "", i => Url.Action("Search", new{	terms = Model.Terms,	page = i}))%>			
			</div>
			<%if ( Model.List.HasNextPage )	 { %>
			<div class="pageLink  ui-corner-all ui-state-default" style="float: left; margin-right: .5em; padding: .2em .8em .2em .8em">
				<a href="<%=Model.List.HasNextPage ? Url.Action("Search", new {terms=Model.Terms, page=Model.List.PageNumber+1}) : "" %>" class="<%=Model.List.HasNextPage ? "" : "ui-state-disabled" %>" title="Next Page"><span class="ui-icon ui-icon-seek-next" style="float: left; margin-right: .1em"></span>Next</a>
			</div>
			<%} %>
			<div class="ui-helper-clearfix">
			</div>
		</div>
	</div>
	<%} %>
	<div id="downloadWindow" title="Download">
	</div>
</asp:Content>
