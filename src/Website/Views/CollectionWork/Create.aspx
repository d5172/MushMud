<%@ PAGE title="" language="C#" masterpagefile="~/Views/Shared/Site.Master" inherits="System.Web.Mvc.ViewPage<MusicCompany.Website.Models.CollectionWork.CreateViewModel>" %>

<ASP:CONTENT contentplaceholderid="AdditionalScripts" runat="server">
	<%=Html.LibraryScript("jquery.validate") %>
	<%=Html.Script("Works.CollectionWorkForm.js") %>
</ASP:CONTENT>
<ASP:CONTENT contentplaceholderid="TitleContent" runat="server">
	Create Collection
</ASP:CONTENT>
<ASP:CONTENT contentplaceholderid="MainContent" runat="server">
<h2>Create New Collection</h2>
	<%Html.RenderPartial("CollectionWorkForm", Model.Form); %>

</ASP:CONTENT>
