<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MusicCompany.Website.Models.Works.IndexViewModel>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	Works by
	<%= Html.Encode(Model.ArtistName) %>
</asp:Content>
<asp:Content ContentPlaceHolderID="AdditionalStylesheets" runat="server">
	<style type="text/css">
		.audioWorkContainer
		{
		}
		.audioWorkEditContainer
		{
		}
		.audioWorkDetailContainer
		{
		}
		.audioWorkToolContainer
		{
		}
		.trackNumber
		{
		}
		.collectionContainer
		{
		}
		.collectionContainerWrapper
		{
		}
		.imgContainer
		{
		}
		.collectionImage
		{
		}
	</style>
</asp:Content>
<asp:Content ContentPlaceHolderID="AdditionalScripts" runat="server">
	<%=Html.LibraryScript("swfobject") %>
	<%=Html.LibraryScript("jquery.validate") %>
	<%=Html.LibraryScript("jquery.ui") %>
	<%=Html.Script("swfupload.min.js") %>
	<%=Html.Script("swfupload.queue.min.js") %>

	<script type="text/javascript">
		var global_SWFUrl = '<%=Url.Content("~/Content/swfupload.swf") %>';
		var global_SWFParam = '<%= Request.Cookies[FormsAuthentication.FormsCookieName].Value %>';
		var global_collectionWorksUrl = '<%=Url.Action("CollectionWorks", "Works", new {artistId = Model.ArtistIdentifier}) %>';
		var global_singleWorksUrl = '<%=Url.Action("ListSingles", "AudioWork", new {artistId = Model.ArtistIdentifier}) %>';
	</script>

	<%=Html.ScriptFromManifest("Works.Index") %>
</asp:Content>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
	<div>
		<div style="float: left">
			<h2>
				Works by
				<%= Html.Encode(Model.ArtistName) %>
			</h2>
		</div>
		<div style="float: right">
			<%= Html.IconLink("Edit Profile","Edit Profile", "ui-icon-person", "", Url.Action( "EditProfile", "Artist", new{artistId = Model.ArtistIdentifier}))%>
		</div>
		<div class="ui-helper-clearfix">
		</div>
	</div>
	<div id="collectionWorks">
		<%Html.RenderPartial("CollectionWorkList", Model.Collections); %>
	</div>
	<div style="padding: 10px 0 10px">
		<div style="float: left; min-width: 140px">
			<%=Html.IconLinkButton("Add a new collection", "Add a new collection", "ui-icon-plusthick", "addCollection", Url.Action("Create", "CollectionWork",new{artistId = Model.ArtistIdentifier}))  %>
		</div>
		<div style="float: left; min-width: 300px;" class="colFormContainer">
			<%if ( Model.Collections.Count() == 0 )
	 { %>
			<div class="helper ui-corner-all" style="max-width: 300px; background-color: #e8eef4; padding: 1em .5em .5em 1em; margin: 0 0 3em 3em">
				<h3>
					<%= Html.Encode(Model.ArtistName) %>
					doesn't have any collections created yet.</h3>
				<p>
					Begin by adding a new collection, then upload song tracks and cover artwork.
				</p>
				<p>
					Or, if you prefer, add an individual song to be released as a single.</p>
			</div>
			<%} %>
		</div>
		<div class="ui-helper-clearfix">
		</div>
	</div>
	<div class="hr">
		<hr />
	</div>
	<div>
	<% string visibility = (Model.Singles.Count() == 0 ? "ui-helper-hidden" : "").ToString(); %>
		<div id="singlesBox" class="<%=visibility %>">
			<h3 style="margin-bottom: .5em">
				Singles</h3>
			<div class="ui-corner-all" style="background-color: #e8eef4; padding: 1em 0 1em; width: 100%" id="singleWorks">
				<%Html.RenderPartial("AudioWorkSingleList", Model.Singles); %>
			</div>
		</div>
	</div>
	<div style="padding: 10px 0 10px">
		<div style="float: left; min-width: 140px">
			<%=Html.IconLinkButton("Add Single Songs", "Add new single songs", "ui-icon-plusthick", "addSingle", Url.Action("CreateSingle", "AudioWork", new{artistId = Model.ArtistIdentifier}))  %>
		</div>
		<div style="float: left; min-width: 300px;">
		</div>
		<div class="ui-helper-clearfix">
		</div>
	</div>
	<%Html.RenderPartial("UploadManager"); %>
	<div id="downloadWindow" title="Download">
	</div>
</asp:Content>
