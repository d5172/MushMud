<%@ CONTROL language="C#" inherits="System.Web.Mvc.ViewUserControl<MusicCompany.Website.Models.AudioWorkForm>" %>
<%Html.BeginForm(
	  "Edit",
	  "AudioWork",
	  new
  {
	  artistId = Model.ArtistId,
	  workId = Model.WorkId,
	  collectionId = Model.CollectionId
  }, FormMethod.Post, new
  {
	  @class = "audioWorkForm",
	  autocomplete = "off"
  }); %>
<div class="commands" style="float: left; min-width: 50px; margin-right: 2px; position: absolute">
	<%--<input type="submit" value="Save" />--%>
	<%=Html.IconLinkButton("Cancel", "Cancel", "ui-icon-close", "cancel", "#") %>
	<%=Html.IconLinkButton("Save", "Save", "ui-icon-check", "save", "#") %>
</div>
<div style="float: left; min-width: 400px; margin-left: 130px; position: absolute">
	<div style="float: left; width: 350px; margin-left:10px">
		<%= Html.TextBox("Title", Model.Title, new{@id="txtTitle", title="Enter the title of the song", @class="text", style="width:320px"}) %>
	</div>
	<div style="float: left">
		<div>
			tags: <%= Html.TextBox("Tags", Model.Tags, new{@id="txtTags", title="Enter multiple tags, separated by commas. For example: rock, male vocal, acoustic", @class="text suggestTags", style="width: 120px"}) %>
		</div>
	</div>
	<div class="ui-helper-clearfix"></div>
</div>
<div class="ui-helper-clearfix">
</div>
<%Html.EndForm(); %>