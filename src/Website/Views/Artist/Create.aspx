<%@ PAGE title="" language="C#" masterpagefile="~/Views/Shared/Site.Master" inherits="System.Web.Mvc.ViewPage<MusicCompany.Website.Models.Artist.CreateViewModel>" %>
<ASP:CONTENT contentplaceholderid="AdditionalScripts" runat="server">
	<script type="text/javascript">
		var global_NameAvailableUrl = '<%=Url.Action("IsNameAvailable", "Artist")%>';
		var global_ExistingArtistId = '';
	</script>
	<%=Html.LibraryScript("jquery.validate") %>
	<%=Html.ScriptFromManifest("Artist.Create")%>
</ASP:CONTENT>
<ASP:CONTENT id="Content1" contentplaceholderid="TitleContent" runat="server">
	Create an Artist profile
</ASP:CONTENT>
<ASP:CONTENT id="Content2" contentplaceholderid="MainContent" runat="server">
	<h2>
		Create an Artist profile</h2>
	Set up an Artist profile. Here you'll enter some information about yourself (or
	or the artist you manage).
	<%Html.RenderPartial("ArtistForm", Model); %>
</ASP:CONTENT>
