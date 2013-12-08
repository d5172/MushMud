<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MusicCompany.Website.Models.Artist.ProfileViewModel>" %>
<div>
	<div style="float: left; margin-right: 1.5em">
		<a href="<%=Url.Action("Artist", "Imagework", new {artistId = Model.Artist.Identifier}) %>" style="border: none" title="Artist Picture: <%= Html.Encode(Model.Artist.Name) %>" class="imageLink">
			<img src="<%=Url.Action("Artist", "Image", new {id = Model.Artist.ProfileImageId, width = Model.ProfilePictureWidth})  %>" width="<%=Model.ProfilePictureWidth %>" style="width: <%=Model.ProfilePictureWidth %>; border: none" alt="Artist Picture: <%= Html.Encode(Model.Artist.Name) %>" title="Artist Picture: <%= Html.Encode(Model.Artist.Name) %>" />
		</a>
	</div>
	<div style="float: left; max-width: 400px;">
		<%= Html.Encode(Model.Artist.Bio).Replace(Environment.NewLine, "<br/>") %>
	</div>
	<div class="ui-helper-clearfix">
	</div>
</div>
