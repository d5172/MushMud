<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MusicCompany.Website.Models.Artist.ProfileViewModel>" %>

<asp:Content ContentPlaceHolderID="AdditionalScripts" runat="server">
	<%=Html.LibraryScript("swfobject") %>
	<%=Html.LibraryScript("jquery.validate") %>
	<%=Html.LibraryScript("jquery.ui") %>
	<%=Html.Script("swfupload.min.js") %>
	<%=Html.Script("swfupload.queue.min.js") %>
	<%=Html.ScriptFromManifest("Artist.EditProfile")%>

	<script type="text/javascript">
		var global_SWFUrl = '<%=Url.Content("~/Content/swfupload.swf") %>';
		var global_SWFParam = '<%= Request.Cookies[FormsAuthentication.FormsCookieName].Value %>';
		var global_NameAvailableUrl = '<%=Url.Action("IsNameAvailable", "Artist")%>';
		var global_ExistingArtistId = '<%=Model.Artist.Identifier%>';
		var global_ArtistBioUrl = '<%=Url.Action("Bio", "Artist", new {artistId = Model.Artist.Identifier})%>';
	</script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Html.FormatTitle("Edit Artist Profile : " + Model.Artist.Name)%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<div>
		<div style="float: left">
			<h2>
				<%= Html.Encode(Model.Artist.Name) %>
			</h2>
		</div>
		<div style="float: right">
			<%= Html.IconLink("Manage Works","Create and edit songs and collections", "ui-icon-wrench", "", Url.Action( "Index", "Works", new{artistId = Model.Artist.Identifier}))%>
		</div>
		<div class="ui-helper-clearfix">
		</div>
	</div>
	<div>
		<div style="float: left" class="optionsBox">
			<div>
				<%=Html.IconLinkButton("Options", "ui-icon-triangle-1-s", "optionsLink", "#Options") %>
				<div class="ui-helper-clearfix">
				</div>
			</div>
			<div class="ui-helper-hidden ui-corner-tr ui-corner-bottom optionsListBox" style="background-color: #fff; border: solid 1px silver; position: absolute; padding: .5em .5em .5em .5em">
				<%=Html.IconLink("Edit Bio", "Edit artist bio", "ui-icon-pencil", "editProfile", Url.Action("BioForm", "Artist",new {artistId = Model.Artist.Identifier}))  %>
				<br />
				<%=Html.IconLink("Change the picture", "Change the profile picture", "ui-icon-image", "editImage", Url.Action("UploadProfileImage", "Artist", new{artistId = Model.Artist.Identifier})) %>
				<br />
			</div>
		</div>
		<div style="float: left;">
			<div>
				<div id="imgBox" style="float: left; min-width: 210px; padding: 3px 3px 35px 3px; margin: 0 10px; text-align: center">
					<img id="profileImage" src="<%=Url.Action("Artist", "Image", new {id = Model.Artist.ProfileImageId, width=200})  %>" class="artistImage" alt="<%= Html.Encode(Model.Artist.Name) %>" style="width: 200px; border: none; display: block;" />
				</div>
				<div style="float: left; max-width: 400px">
					<div id="viewBox">
						<%if (string.IsNullOrEmpty(Model.Artist.Bio))
		{ %>
						<div id="emptyBio" class="ui-corner-all" style="background-color: #e8eef4; padding: 1em 1em 1em 1em;">
							<h3>
								You don't have a Bio entered yet.</h3>
							Click here to start filling out your artist profile.
						</div>
						<%} %>
						<%Html.RenderPartial("Bio", Model); %>
					</div>
					<div id="editBox" class="ui-helper-hidden">
					</div>
				</div>
				<div class="ui-helper-clearfix">
				</div>
			</div>
		</div>
		<div class="ui-helper-clearfix">
		</div>
	</div>
	<%Html.RenderPartial("UploadManager"); %>
</asp:Content>
