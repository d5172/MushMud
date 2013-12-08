<%@ CONTROL language="C#" inherits="System.Web.Mvc.ViewUserControl<MusicCompany.Website.Models.Artist.BioFormViewModel>" %>

<form method="post" action="<%= Url.Action("BioForm", "Artist", new {artistId = Model.Command.Identifier}) %>" id="bioForm">
<ul>
	<li>
		<label for="Bio" class="desc">
			About <%=Html.Encode(Model.ArtistName) %>:</label>
		<div>
			<%=Html.TextArea("Bio", Model.Command.Bio, new{	title = "Enter the artist bio",	@class = "textarea", rows="8"})%>
		</div>
	</li>
	<li>
		<div class="commands">
			<%=Html.IconButton("Save", "ui-icon-check", "", "submit") %>
			or
			<%=Html.ActionLink("Cancel", "Index", "YourMusic", new {@class="cancel"}) %>
		</div>
	</li>
</ul>
</form>
