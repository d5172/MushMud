<%@ PAGE title="" language="C#" masterpagefile="~/Views/Shared/Site.Master" inherits="System.Web.Mvc.ViewPage<MusicCompany.Website.Models.AudioWorkForm>" %>

<ASP:CONTENT id="Content1" contentplaceholderid="HeadContent" runat="server">
	<% Html.RenderPartial("AudioWorkSupportScripts", Model); %>
	<script type="text/javascript" src="<%=Url.Content("~/Scripts/jquery.validate.min.js") %>"></script>
</ASP:CONTENT>
<ASP:CONTENT id="Content2" contentplaceholderid="TitleContent" runat="server">
	Edit
</ASP:CONTENT>
<ASP:CONTENT id="Content3" contentplaceholderid="MainContent" runat="server">
	<h2>
		Edit Song</h2>
		<%Html.RenderPartial("AudioWorkInlineForm", Model); %>
		
	<%--<% using (Html.BeginForm("Edit", "AudioWork", new{artistId = Model.ArtistId, workId=Model.WorkId}, FormMethod.Post, new	{ enctype = "multipart/form-data" }))
	{%>
	<% Html.RenderPartial("AudioWorkFieldSet", Model); %>
	<% } %>
	<div>
		<%=Html.ActionLink("Cancel", "Index", "Works", new{artistId = Model.ArtistId}, null)%>
	</div>--%>
</ASP:CONTENT>
