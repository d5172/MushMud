<%@ CONTROL language="C#" inherits="System.Web.Mvc.ViewUserControl<MusicCompany.Website.Models.AudioWorkDTO>" %>
<fieldset>
	<legend>Audio Work Properties</legend>
	<div>
		<div id="divAlternateContent">
			<label for="postedFile">
				Audio File:
			</label>
			<input type="file" id="postedFile" name="postedFile" />
		</div>
		<div>
		</div>
		<div id="divSWFUploadUI">
			<div>
				<label for="txtFileName">
					Audio File:
				</label>
				<input type="text" id="txtFileName" disabled="disabled" style="border: solid 1px;
					background-color: #FFFFFF;" />
				<span id="spanButtonPlaceholder"></span>
			</div>
			<div class="flash" id="fsUploadProgress">
			</div>
		</div>
		<%= Html.Hidden("FileId", null, new{name="FileId"})%>
	</div>
	<div>
		<label for="Title">
			Title:</label>
		<%= Html.TextBox("Title")%>
	</div>
	<div>
		<label for="Description">
			Description:</label>
		<%= Html.TextArea("Description", Model.Description, 5, 35, null) %>
	</div>
	<div>
		<label for="CollectionName">
			Include in Collection:</label>
		<%= Html.DropDownList("CollectionName", Model.AvailableCollections, "(None)") %>
	</div>
	<div>
		<label for="Tags">
			Tags:</label>
		<%= Html.TextBox("Tags", Model.Tags, new{id = "txtTags"})%>
	</div>
	<div>
		<input type="submit" value="Save" id="btnSubmit" />
	</div>
</fieldset>
