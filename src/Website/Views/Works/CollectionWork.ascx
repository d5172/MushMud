<%@ CONTROL language="C#" inherits="System.Web.Mvc.ViewUserControl<MusicCompany.Common.ViewModel.CollectionSummaryView>" %>
<div class="collectionBox" style="padding-bottom: 2em">
	<div style="float: left" class="optionsBox">
		<div>
			<%=Html.IconLinkButton("Options", "ui-icon-triangle-1-s", "optionsLink", "#Options") %>
			<div class="ui-helper-clearfix">
			</div>
		</div>
		<div class="ui-helper-hidden ui-corner-tr ui-corner-bottom optionsListBox" style="background-color: #fff; border: solid 1px silver; position: absolute; padding: .5em .5em .5em .5em">
			<%=Html.IconLink("Edit", "Edit the collection", "ui-icon-pencil", "editCollection", Url.Action("Edit", "CollectionWork",new {artistId = Model.ArtistIdentifier, workId = Model.Identifier}))  %>
			<br />
			<%=Html.IconLink("Add Songs", "Add Songs", "ui-icon-plus", "addSongs", Url.Action("CreateInCollection", "AudioWork", new{artistId = Model.ArtistIdentifier, workId = Model.Identifier})) %>
			<br />
			<%=Html.IconLink("Change the picture", "Change the picture", "ui-icon-image", "editImage", Url.Action("UploadImage", "CollectionWork", new{artistId = Model.ArtistIdentifier,	workId = Model.Identifier})) %>
			<br />
			<% var disabled = "";
	  if (Model.BinaryFileId == Guid.Empty)
		  disabled = "ui-state-disabled";%>
			<%=Html.IconLink("Remove the picture", "Remove the picture", "ui-icon-close", disabled + " deleteImage", Url.Action("RemoveImage", "CollectionWork", new{	artistId = Model.ArtistIdentifier, workId = Model.Identifier}))%>
			<br />
			<%=Html.IconLink("Delete", "Delete the collection", "ui-icon-trash", "deleteCollection", Url.Action("Delete", "CollectionWork", new{artistId = Model.ArtistIdentifier, workId = Model.Identifier})) %>
		</div>
	</div>
	<div style="float: left">
		<div>
			<div class="imgContainer" style="float: left; min-width: 130px; padding: 3px 3px 35px 3px; margin: 0 10px; text-align: center">
				<img src="<%=Url.Action("Collection", "Image", new {id = Model.BinaryFileId, width=115})  %>" class="collectionImage" alt="<%= Html.Encode(Model.Title) %> by <%= Html.Encode(Model.ArtistName) %>" id="im<%=Model.Identifier %>" style="width: 115px; height: 115px; border: none; display: block;" />
			</div>
			<div class="collectionContainerWrapper" style="float: left; padding: 0px 3px 0px 3px; min-width: 500px;">
				<div class="collectionContainer">
					<div class="collectionViewContainer">
						<div class="collectionTitle">
							<%= Html.Encode(Model.Title) %>
							<%if ( Model.ReleaseDate > DateTime.Now )
		 { %>
							<span>(Not Yet Released)</span> 
		 <%} %>
						</div>
						<p>
							<%= Html.Encode(Model.Description) %>
						</p>
						<p>
							Release Date:
							<%=Html.Encode(Model.ReleaseDate.ToShortDateString()) %>
							<br />
							Total Time:
							<%=Html.DisplayDuration(Model.Seconds) %>
							<br />
							License:
							<%=Html.Encode(Model.License.Name) %>
							<br />
							<%=Html.DisplayTags(Model.Tags, "Tags: ") %>
							
						</p>
					</div>
					<div class="collectionEditContainer" style="display: none">
					</div>
					<input type="hidden" class="audioListRoute" value="<%=Url.Action("ListInCollection", "AudioWork", new{artistId = Model.ArtistIdentifier, workId = Model.Identifier})%>" />
					<input type="hidden" class="audioSortRoute" value="<%=Url.Action("SortChildWorksByIds", "Works", new{artistId = Model.ArtistIdentifier, workId = Model.Identifier})%>" />
					<div class="audioListContainer ui-corner-all" style="background-color: #e8eef4; padding: 1em 0 1em; width:100%" id="al<%=Model.Identifier %>">
						<%Html.RenderPartial("AudioWorkList", Model.Tracks);%>
					</div>
					<div style="padding-top: 1em;">
						<div class="toolLinks" style="height: 3em;">
							<%=Html.IconLinkButton("Add Songs", "Add Songs", "ui-icon-plusthick", "add addTracks", Url.Action("CreateInCollection", "AudioWork", new{artistId = Model.ArtistIdentifier, workId = Model.Identifier})) %>
						</div>
					</div>
				</div>
			</div>
			<div class="ui-helper-clearfix">
			</div>
		</div>
	</div>
	<div class="ui-helper-clearfix">
	</div>
</div>
