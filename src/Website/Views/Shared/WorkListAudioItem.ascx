<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MusicCompany.Common.ViewModel.AudioSingleSummaryView>" %>
<div style="margin-bottom: 10px;">
	<div style="float: left; min-width: 140px; max-width: 140px">
		<div>
			<a href="<%=Url.Action("Artist", "Imagework", new {artistId = Model.ArtistIdentifier}) %>" style="border: none" title="Artist Picture:<%= Html.Encode(Model.ArtistName) %>" class="imageLink">
				<img src="<%=Url.Action("Artist", "Image", new {id = Model.ArtistImageId, width=115})  %>" width="115" height="115" style="width: 115; height: 115; border: none" alt="Artist Picture: <%= Html.Encode(Model.ArtistName) %>" title="Artist Picture: <%= Html.Encode(Model.ArtistName) %>" />
			</a>
		</div>
		<div style="padding-left: 15px">
			<%= Html.IconLinkButton("Download", "Download " + Model.Title, "ui-icon-circle-arrow-s", "downloadButton ui-priority-secondary", Url.Action("DownloadSingle", "Music", new{artistId = Model.ArtistIdentifier, workId=Model.Identifier })) %>
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
			<%= Html.ActionLink("Read More", "Single", "Music", new{artistId = Model.ArtistIdentifier, workId=Model.Identifier}, new{@class="expandDescription"})%>
			<%} %>
		</div>
		<%} %>
		<div style="margin-bottom: .5em">
			<em>
				<%=Html.Encode(Model.Tags) %></em>
		</div>
		<%Html.RenderPartial("SinglePlayer", Model);%>
	</div>
	<div style="float: left; min-width: 75px; max-width: 120px;">
		(Single)<div>
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