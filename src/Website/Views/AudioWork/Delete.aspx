<%@ PAGE title="" language="C#" masterpagefile="~/Views/Shared/Site.Master" inherits="System.Web.Mvc.ViewPage<MusicCompany.Website.Models.AudioWorkDTO>" %>

<ASP:CONTENT id="Content1" contentplaceholderid="TitleContent" runat="server">
	Delete Song
</ASP:CONTENT>
<ASP:CONTENT id="Content2" contentplaceholderid="MainContent" runat="server">
	<h2>
		Delete Song</h2>
	Are you sure you want to delete
	<%= Html.Encode(Model.Title) %>?
	<div>
		<%using (Html.BeginForm())
	  { %>
		<input id="confirmButton" name="confirmButton" type="submit" value="Yes, Delete" />
		<%} %>
	</div>
	<div>
		<%=Html.ActionLink("Cancel", "Index", "Works", new{artistId = Model.ArtistId}, null)%>
	</div>
</ASP:CONTENT>
<ASP:CONTENT id="Content3" contentplaceholderid="HeadContent" runat="server">
</ASP:CONTENT>
