<%@ PAGE title="" language="C#" masterpagefile="~/Views/Shared/Site.Master" inherits="System.Web.Mvc.ViewPage<MusicCompany.Website.Models.CollectionWork.EditViewModel>" %>

<ASP:CONTENT contentplaceholderid="AdditionalScripts" runat="server">
	<%=Html.LibraryScript("jquery.validate") %>
	<%=Html.LibraryScript("jquery.ui") %>
	<%=Html.ScriptFromManifest("CollectionWork.Edit")%>
</ASP:CONTENT>
<ASP:CONTENT contentplaceholderid="TitleContent" runat="server">
	Edit Collection
</ASP:CONTENT>
<ASP:CONTENT contentplaceholderid="MainContent" runat="server">
	<h2>
		Edit Collection</h2>
	<%Html.RenderPartial("CollectionWorkForm", Model.Form); %>
</ASP:CONTENT>
