<%@ CONTROL language="C#" inherits="System.Web.Mvc.ViewUserControl<MusicCompany.Website.Models.CollectionWork.CollectionWorkFormViewModel>" %>
<% using (Html.BeginForm(Model.FormAction, "CollectionWork", new
   {
	   artistId = Model.ArtistIdentifier,
	   workId = Model.CollectionIdentifier
   }, FormMethod.Post, new
   {
	   @class = "collectionWorkForm",
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
			Description:</label>
		<div>
			<%= Html.TextArea("Description", Model.Command.Description, new{@id="txtDescription", title="Enter a description", @class="text textarea", rows="4", style="width:345px;"}) %>
		</div>
	</li>
	<li>
		<label for="ReleaseDate" class="desc">
			Release Date:</label>
		<div>
			<%= Html.TextBox("ReleaseDate", Model.Command.ReleaseDate.ToShortDateString(), new{@id="txtReleaseDate", title="Enter the release date", @class="text dateInput", style="width:78px;"}) %>
		</div>
	</li>
	<li>
		<label for="LicenseIdentifier" class="desc">
			License:</label>
		<div>
			<div style="float: left">
				<%=Html.DropDownList("LicenseIdentifier", Model.AvailableLicenses, new{title = "Select a License", @class="select"})%>
			</div>
			<div style="float: left; margin-left: 5px;">
				<%=Html.IconLink("Help", "Help on choosing a license", "ui-icon-help", "licenseHelp", Url.Action("Licenses", "Works"))  %>
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
			<%=Html.IconButton("Save", "ui-icon-check", "", "submit") %>
			or
			<%=Html.ActionLink("Cancel", "Index", "Works", new{artistId = Model.ArtistIdentifier}, new {@class="cancel"}) %>
		</div>
	</li>
</ul>
<%} %>