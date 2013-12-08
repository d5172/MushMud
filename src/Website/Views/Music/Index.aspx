<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MusicCompany.Website.Models.Music.IndexViewModel>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	<%=Html.FormatTitle("Music - List of all titles") %>
</asp:Content>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
<meta name="ROBOTS" content="INDEX, FOLLOW" />
<meta name="description" content="Lists all titles available on MushMud.com" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="AdditionalStylesheets">
<style type="text/css">
.workListItemBoxWidth
{
	width:450px;
}
</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="AdditionalScripts">
	<%=Html.LibraryScript("jquery.ui") %>
	<%=Html.Script("jquery.jplayer.min.js") %>
	<%=Html.ScriptFromManifest("Music.Index") %>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
	<%Html.RenderPartial("SearchPartial"); %>
	<h1>
		Music</h1>
	<%Html.RenderPartial("WorkList", Model.Works); %>
	<div>
		<div style="font-weight: bold; margin-bottom: .5em">
			Works
			<%=Model.Works.StartItem() %>-<%=Model.Works.EndItem() %>
			of
			<%=Model.Works.TotalItemCount %>
		</div>
		<div>
			<div class="pageLink ui-corner-all ui-state-default" style="float: left; margin-right: .5em; padding: .2em .8em .2em .8em">
				<a href="<%=Url.Action("Index", new {page=1}) %>" class="<%=Model.Works.IsFirstPage ? "ui-state-disabled" : "" %>" title="First Page"><span class="ui-icon ui-icon-seek-first" style="float: right; margin-left: .1em"></span>First</a>
			</div>
			<div class="pageLink  ui-corner-all ui-state-default" style="float: left; margin-right: .5em; padding: .2em .8em .2em .8em">
				<a href="<%= Model.Works.HasPreviousPage ? Url.Action("Index", new {page=Model.Works.PageNumber-1}) : "" %>" class="<%=Model.Works.HasPreviousPage ? "" : "ui-state-disabled" %>" title="Previous Page"><span class="ui-icon ui-icon-seek-prev" style="float: right; margin-left: .1em"></span>Prev</a>
			</div>
			<div class="pageLink  ui-corner-all ui-state-default" style="float: left; margin-right: .5em; padding: .2em .8em .2em .8em">
				<a href="<%=Model.Works.HasNextPage ? Url.Action("Index", new {page=Model.Works.PageNumber+1}) : "" %>" class="<%=Model.Works.HasNextPage ? "" : "ui-state-disabled" %>" title="Next Page"><span class="ui-icon ui-icon-seek-next" style="float: left; margin-right: .1em"></span>Next</a>
			</div>
			<div class="pageLink  ui-corner-all ui-state-default" style="float: left; padding: .2em .8em .2em .8em">
				<a href="<%=Url.Action("Index", new {page=Model.Works.PageCount}) %>" class="<%=Model.Works.IsLastPage ? "ui-state-disabled" : "" %>" title="Last Page"><span class="ui-icon ui-icon-seek-end" style="float: left; margin-right: .1em"></span>Last</a>
			</div>
			<div class="ui-helper-clearfix">
			</div>
		</div>
	</div>
	<div id="downloadWindow" title="Download">
	</div>
	<div id="lightBox" title="ViewImage">
	</div>
</asp:Content>
