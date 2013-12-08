<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MusicCompany.Website.Models.Person.ProfileViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Html.FormatTitle(Model.Person.Username + " - Profile") %>
</asp:Content>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
	<meta name="ROBOTS" content="INDEX, FOLLOW" />
	<meta name="description" content="User Profile: <%= Html.Encode(Model.Person.Username) %>" />
	<meta name="keywords" content="music sharing streaming download downloads mp3 flac audio torrent community" />
</asp:Content>
<asp:Content ContentPlaceHolderID="AdditionalScripts" runat="server">
	<%=Html.LibraryScript("jquery.ui") %>
	<%=Html.Script("jquery.jplayer.min.js") %>
	<%=Html.ScriptFromManifest("Person.Profile")%>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="AdditionalStylesheets">
	<style type="text/css">
		.workListItemBoxWidth
		{
			width: 400px;
		}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<div style="margin-bottom: 2em; margin-top: 1em">
		<div>
			<div style="float: left; margin-right: 1.5em">
				<img src="<%=Url.Action("Person", "Image", new {id = Model.Person.ProfileImageId, width = 100})  %>" width="100" style="width: 100px; border: none" alt="Profile Picture: <%= Html.Encode(Model.Person.Name) %>" title="Profile Picture: <%= Html.Encode(Model.Person.Name) %>" />
			</div>
			<div style="float: left; max-width: 400px;">
				<h2>
					<%= Html.Encode(Model.Person.Username) %></h2>
				<%--<%= Html.Encode(Model.Person.Bio).Replace(Environment.NewLine, "<br/>") %>--%>
			</div>
			<div class="ui-helper-clearfix">
			</div>
		</div>
	</div>
	<%if ( Model.Works.Count > 0 )
   { %>
	<div>
		<div style="margin-bottom: 2em">
			<h3>
				Works shared by
				<%= Html.Encode(Model.Person.Username) %></h3>
		</div>
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
					<a href="<%=Url.Action("Profile", "Person", new {username=Model.Person.Username, page=1}, null) %>" class="<%=Model.Works.IsFirstPage ? "ui-state-disabled" : "" %>" title="First Page"><span class="ui-icon ui-icon-seek-first" style="float: right; margin-left: .1em"></span>First</a>
				</div>
				<div class="pageLink  ui-corner-all ui-state-default" style="float: left; margin-right: .5em; padding: .2em .8em .2em .8em">
					<a href="<%= Model.Works.HasPreviousPage ? Url.Action("Profile", "Person", new {username=Model.Person.Username, page=Model.Works.PageNumber-1}, null) : "" %>" class="<%=Model.Works.HasPreviousPage ? "" : "ui-state-disabled" %>" title="Previous Page"><span class="ui-icon ui-icon-seek-prev" style="float: right; margin-left: .1em"></span>Prev</a>
				</div>
				<div class="pageLink  ui-corner-all ui-state-default" style="float: left; margin-right: .5em; padding: .2em .8em .2em .8em">
					<a href="<%=Model.Works.HasNextPage ? Url.Action("Profile", "Person", new {username=Model.Person.Username, page=Model.Works.PageNumber+1}, null) : "" %>" class="<%=Model.Works.HasNextPage ? "" : "ui-state-disabled" %>" title="Next Page"><span class="ui-icon ui-icon-seek-next" style="float: left; margin-right: .1em"></span>Next</a>
				</div>
				<div class="pageLink  ui-corner-all ui-state-default" style="float: left; padding: .2em .8em .2em .8em">
					<a href="<%=Url.Action("Profile", "Person", new {username=Model.Person.Username, page=Model.Works.PageCount}, null) %>" class="<%=Model.Works.IsLastPage ? "ui-state-disabled" : "" %>" title="Last Page"><span class="ui-icon ui-icon-seek-end" style="float: left; margin-right: .1em"></span>Last</a>
				</div>
				<div class="ui-helper-clearfix">
				</div>
			</div>
		</div>
	</div>
	<%} %>
	<div id="downloadWindow" title="Download">
	</div>
	<div id="lightBox" title="Image">
	</div>
</asp:Content>
