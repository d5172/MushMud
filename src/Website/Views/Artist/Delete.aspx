<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MusicCompany.Website.Models.Artist.DeleteViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Delete Artist Profile
</asp:Content>
<ASP:CONTENT contentplaceholderid="AdditionalScripts" runat="server">
	<%=Html.ScriptFromManifest("Artist.Delete")%>
</ASP:CONTENT>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Delete Artist Profile</h2>
	
	<p>Are you sure you want to delete the <%=Html.Encode(Model.Artist.Name) %> profile?
		(This will delete all the songs, videos, collections and stats!)
	</p>
	<div>
		<%using (Html.BeginForm())
	  { %>
		<input id="confirmButton" name="confirmButton" type="submit" value="Yes, Delete" />
		<%} %>
	</div>
	<div>
		<%=Html.ActionLink("Cancel", "Index", "YourMusic", new {artistId = Model.Artist.Identifier}, null)%>
	</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
