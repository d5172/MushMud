<%@ CONTROL language="C#" inherits="System.Web.Mvc.ViewUserControl<MusicCompany.Website.Models.Artist.CreateViewModel>" %>

<form method="post" action="#" id="artistForm" autocomplete="false">
<ul>
	<li>
		<label for="Name" class="desc">
			Name:</label>
		<div>
			<%= Html.TextBox("ArtistName", Model.Command.ArtistName, new{ title="Enter the artist name", @class="text"})%>
		</div>
	</li>
	<li>
		<label for="Bio" class="desc">
			Bio:</label>
		<div>
			<%=Html.TextArea("Bio", Model.Command.Bio, new{	title = "Enter the artist bio",	@class = "textarea", rows="6"})%>
		</div>
	</li>
	<li>
		<div class="commands">
			<%=Html.IconButton("Save", "ui-icon-check", "", "submit") %>
			or
			<%=Html.ActionLink("Cancel", "Index", "YourMusic", null, new {@class="cancel"}) %>
		</div>
	</li>
</ul>
</form>
