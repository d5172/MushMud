<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MusicCompany.Website.Models.AudioWork.EditSingleViewModel>" %>
<% using (Html.BeginForm("EditSingle", "AudioWork", new
   {
	   artistId = Model.Command.ArtistIdentifier,
	   workId = Model.Command.WorkIdentifier
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
		<label for="ReleaseDate" class="desc">
			Release Date:</label>
		<div>
			<%= Html.TextBox("ReleaseDate", Model.Command.ReleaseDate.ToShortDateString(), new{	@id = "txtReleaseDate", title = "Enter the release date", @class = "text", style = "width:78px;"})%>
		</div>
	</li>
	<li>
		<label for="LicenseIdentifier" class="desc">
			License:</label>
		<div>
			<div style="float: left">
				<%=Html.DropDownList("LicenseIdentifier", Model.AvailableLicenses, new { title = "Select a License", @class = "select" })%>
			</div>
			<div style="float: left; margin-left: 5px;">
				<%=Html.IconLink("Help", "Help on choosing a license", "ui-icon-help", "licenseHelp", Url.Action("Licenses", "Works"))%>
			</div>
			<div class="ui-helper-clearfix" />
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