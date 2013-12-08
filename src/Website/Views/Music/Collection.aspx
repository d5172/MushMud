<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MusicCompany.Website.Models.Music.CollectionViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Html.FormatTitle(Model.Collection.Title + " by " + Model.Collection.ArtistName)%>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="AdditionalScripts">
	<%=Html.LibraryScript("jquery.ui") %>
	<%=Html.Script("jquery.jplayer.min.js") %>
	<%=Html.ScriptFromManifest("Music.Collection") %>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="AdditionalStylesheets">
	<style type="text/css">
		.workListItemBoxWidth
		{
			width: 450px;
		}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<%Html.RenderPartial("SearchPartial"); %>
	<%Html.RenderPartial("WorkListCollectionItem", Model.Collection); %>
	<div style="background-color: #e8eef4; padding: .5em; margin-bottom:1em" class="ui-corner-all">
		<div style="text-align: center">
			<%Html.RenderPartial("LicenseLine", Model.Collection); %>
		</div>
	</div>
	<%=Html.ActionLink("More from " + Model.Collection.ArtistName, "Profile", "Artist", new {artistId = Model.Collection.ArtistIdentifier}, null) %>
	<div id="downloadWindow" title="Download">
	</div>
	<div id="lightBox" title="ViewImage">
	</div>
</asp:Content>
