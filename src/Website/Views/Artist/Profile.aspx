<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MusicCompany.Website.Models.Artist.ProfileViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Html.FormatTitle(Model.Artist.Name + " - Artist Profile") %>
</asp:Content>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
	<meta name="ROBOTS" content="INDEX, FOLLOW" />
	<meta name="description" content="Artist Profile: <%= Html.Encode(Model.Artist.Name) %>" />
	<meta name="keywords" content="music sharing streaming download downloads mp3 flac audio torrent community" />
</asp:Content>
<asp:Content ContentPlaceHolderID="AdditionalScripts" runat="server">
	<%=Html.LibraryScript("jquery.ui") %>
	<%=Html.Script("jquery.jplayer.min.js") %>
	<%=Html.ScriptFromManifest("Artist.Profile")%>
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
	<h1>
		<%= Html.Encode(Model.Artist.Name) %></h1>
	<div style="margin-bottom: 2em; margin-top: 1em">
		<%Html.RenderPartial("ProfileDetail", Model); %>
	</div>
	<%if ( Model.Artist.TopLevelWorks.Count() > 0 )
   { %>
	<div>
		<h3>
			Works by
			<%= Html.Encode(Model.Artist.Name)%>
		</h3>
		<%Html.RenderPartial("WorkList", Model.Artist.TopLevelWorks); %>
	</div>
	<%} %>
	<div id="downloadWindow" title="Download">
	</div>
	<div id="lightBox" title="Image">
	</div>
</asp:Content>
