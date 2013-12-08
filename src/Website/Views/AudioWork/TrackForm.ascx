<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MusicCompany.Website.Models.AudioWork.EditTrackViewModel>" %>
<% using (Html.BeginForm("EditTrack", "AudioWork", new
   {
	   artistId = Model.Command.ArtistIdentifier,
	   workId = Model.Command.WorkIdentifier,
	   collectionId = Model.Command.CollectionIdentifier
   }, FormMethod.Post, new
   {
	   @class = "audioWorkForm",
	   autocomplete = "off"
   }))
   {  %>
<ul>
	<li>
		<label for="Title" class="desc">
			Title:</label>
		<div>
			<%= Html.TextBox("Title", Model.Command.Title, new{@id="txtTitle", title="Enter a title", @class="text", style="width:345px;"}) %>
		</div>
	</li>
	<li>
		<label for="Description" class="desc">
			Notes:</label>
		<div>
			<%= Html.TextArea("Description", Model.Command.Description, new{@id="txtDescription", title="Enter a description", @class="text textarea", rows="4", style="width:345px;"}) %>
		</div>
	</li>
	
	<li>
		<label for="Tags" class="desc">
			Tags:</label>
		<div>
			<%= Html.TextBox("Tags", Model.Command.Tags, new{@id="txtTags", title="Enter multiple tags, separated by commas. For example: rock, male vocal, acoustic", @class="text suggestTags"}) %>
		</div>
	</li>
	<li>
		<div class="commands">
			<%=Html.IconButton("Save Changes", "ui-icon-check", "", "submit") %>
			or
			<%=Html.ActionLink("Cancel", "Index", "Works", new{artistId = Model.Command.ArtistIdentifier}, new {@class="cancel"}) %>
		</div>
	</li>
</ul>
<%} %>