<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MusicCompany.Website.Models.AudioWorkForm>" %>
<% using (Html.BeginForm(Model.FormAction, "AudioWork", new
   {
	   artistId = Model.ArtistId,
	   workId = Model.WorkId,
	   collectionId = Model.CollectionId
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
			<%= Html.TextBox("Title", Model.Title, new{@id="txtTitle", title="Enter a title", @class="text", style="width:345px;"}) %>
		</div>
	</li>
	<li>
		<label for="Description" class="desc">
			Notes:</label>
		<div>
			<%= Html.TextArea("Description", Model.Description, new{@id="txtDescription", title="Enter a description", @class="text textarea", rows="4", style="width:345px;"}) %>
		</div>
	</li>
	<%if (Model.CanChangeReleaseDate)
   { %>
	<li>
		<label for="ReleaseDate" class="desc">
			Release Date:</label>
		<div>
			<%= Html.TextBox("ReleaseDate", Model.ReleaseDate.ToShortDateString(), new{	@id = "txtReleaseDate", title = "Enter the release date", @class = "text", style = "width:78px;"})%>
		</div>
	</li>
	<%} %>
	<%if (Model.CanChangeLicense)
   { %>
	<li>
		<label for="License" class="desc">
			License:</label>
		<div>
			<div style="float: left">
				<%=Html.DropDownList("License", Model.AvailableLicenses, new { title = "Select a License", @class = "select" })%>
			</div>
			<div style="float: left; margin-left: 5px;">
				<%=Html.IconLink("Help", "Help on choosing a license", "ui-icon-help", "licenseHelp", Url.Action("Licenses", "Works"))%>
			</div>
			<div class="ui-helper-clearfix" />
		</div>
	</li>
	<%} %>
	<li>
		<label for="Tags" class="desc">
			Tags:</label>
		<div>
			<%= Html.TextBox("Tags", Model.Tags, new{@id="txtTags", title="Enter multiple tags, separated by commas. For example: rock, male vocal, acoustic", @class="text suggestTags"}) %>
		</div>
	</li>
	<li>
		<div class="commands">
			<%=Html.IconButton("Save Changes", "ui-icon-check", "", "save") %>
			or
			<%=Html.ActionLink("Cancel", "Index", "Works", new{artistId = Model.ArtistId}, new {@class="cancel"}) %>
		</div>
	</li>
</ul>
<%} %>