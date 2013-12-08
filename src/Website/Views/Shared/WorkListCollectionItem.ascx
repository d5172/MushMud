<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MusicCompany.Common.ViewModel.CollectionSummaryView>" %>
<div style="margin-bottom: 10px;">
	<div style="float: left; min-width: 140px; max-width: 140px">
		<div>
			<a href="<%=Url.Action("Collection", "Imagework", new {artistId = Model.ArtistIdentifier, workId = Model.Identifier}) %>" style="border: none" title="Collection Artwork: <%= Html.Encode(Model.Title) %>" class="imageLink">
				<img src="<%=Url.Action("Collection", "Image", new {id = Model.BinaryFileId, width=115})  %>" width="115" height="115" style="width: 115; height: 115; border: none" alt="Collection Artwork: <%= Html.Encode(Model.Title) %>" title="Collection Artwork: <%= Html.Encode(Model.Title) %>" />
			</a>
		</div>
		<div style="padding-left: 15px">
			<%= Html.IconLinkButton("Download", "Download " + Model.Title, "ui-icon-circle-arrow-s", "downloadButton ui-priority-secondary", Url.Action("DownloadCollection", "Music", new{artistId = Model.ArtistIdentifier, workId=Model.Identifier })) %>
		</div>
	</div>
	<div style="float: left; margin-right: 24px" class="collectionContainer workListItemBoxWidth">
		<div class="collectionTitle">
			<%=Model.Title %>
		</div>
		<div style="margin-bottom: .5em;">
			by
			<%= Html.ActionLink(Model.ArtistName, "Profile", "Artist", new{artistId= Model.ArtistIdentifier}, new{@class="expandProfile"})%>
			<div class="profileContainer ui-helper-hidden" style="padding: 1em 0 .5em 1em">
			</div>
		</div>
		<%if ( !string.IsNullOrEmpty(Model.Description) )
	{ %>
		<div style="margin-bottom: 1em">
			<div class="descriptionContainer ui-helper-hidden">
			</div>
			<span class="descriptionTruncated">
				<%= Html.Truncate(Model.Description, 100) %></span>
			<%if ( Model.Description.Length > 150 )
	 { %>
			<%= Html.ActionLink("Read More", "Collection", "Music", new{artistId = Model.ArtistIdentifier, workId=Model.Identifier}, new{@class="expandDescription"})%>
			<%} %>
		</div>
		<%} %>
		<div style="margin-bottom: .5em">
			<em>
				<%=Html.Encode(Model.Tags) %></em>
		</div>
		<%Html.RenderPartial("CollectionPlayer", Model);%>
	</div>
	<div style="float: left; min-width: 75px; max-width: 120px;">
		<div>
			Rank: #<%=Model.Rank%>
		</div>
		<div>
			Released
			<%=Model.ReleaseDate.ToShortDateString() %>
		</div>
		<div>
			<%=Html.DisplayStats(Model.DownloadCount, Model.PlayCount)%>
		</div>
		<div>
			<%Html.RenderPartial("LicenseLink", Model.License); %>
		</div>
	</div>
	<div class="ui-helper-clearfix">
	</div>
</div>
<div class="ui-helper-clearfix">
</div>