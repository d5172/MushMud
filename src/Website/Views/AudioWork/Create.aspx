<%@ PAGE title="" language="C#" masterpagefile="~/Views/Shared/Site.Master" inherits="System.Web.Mvc.ViewPage<MusicCompany.Website.Models.AudioWorkDTO>" %>

<ASP:CONTENT id="Content1" contentplaceholderid="HeadContent" runat="server">
	<script type="text/javascript" src="<%=Url.Content("~/Scripts/jquery.validate.min.js") %>"></script>
	<% Html.RenderPartial("AudioWorkSupportScripts", Model); %>
</ASP:CONTENT>
<ASP:CONTENT id="Content2" contentplaceholderid="TitleContent" runat="server">
	Create New Song
</ASP:CONTENT>
<ASP:CONTENT id="Content3" contentplaceholderid="MainContent" runat="server">
	<% using (Html.BeginForm("Create", "AudioWork", new{artistId = Model.ArtistId}, FormMethod.Post, new	{ @id="frmAudioWork", enctype = "multipart/form-data" }))
	{%>
	<% Html.RenderPartial("AudioWorkFieldSet", Model); %>
	<% } %>
	<div>
		<%=Html.ActionLink("Cancel", "Index", "Works", new{artistId = Model.ArtistId}, null)%>
	</div>
</ASP:CONTENT>
