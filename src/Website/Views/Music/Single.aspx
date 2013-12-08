<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MusicCompany.Website.Models.Music.SingleViewModel>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	<%=Html.FormatTitle(Model.Single.Title + " by " + Model.Single.ArtistName) %>
</asp:Content>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
	<meta name="ROBOTS" content="INDEX, FOLLOW" />
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="AdditionalStylesheets">
	<style type="text/css">
		.workListItemBoxWidth
		{
			width: 450px;
		}
	</style>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="AdditionalScripts">
	<%=Html.LibraryScript("jquery.ui") %>
	<%=Html.Script("jquery.jplayer.min.js") %>
	<%=Html.ScriptFromManifest("Music.Single") %>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
	<%Html.RenderPartial("SearchPartial"); %>
	<div>
		<%Html.RenderPartial("WorkListAudioItem", Model.Single); %>
	</div>
	<div style="background-color: #e8eef4; padding: .5em; margin-bottom:1em" class="ui-corner-all">
		<div style="text-align: center">
			<%Html.RenderPartial("LicenseLine", Model.Single); %>
		</div>
	</div>
	<%=Html.ActionLink("More from " + Model.Single.ArtistName, "Profile", "Artist", new {artistId = Model.Single.ArtistIdentifier}, null) %>
	<div id="downloadWindow" title="Download">
	</div>
	<div id="lightBox" title="ViewImage">
	</div>
</asp:Content>
