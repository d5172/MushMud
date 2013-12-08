<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MusicCompany.Website.Models.Home.IndexViewModel>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
<%=Html.FormatTitle("A Music Sharing Community Featuring Free and Legal Music Downloads")%>
</asp:Content>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
<meta name="ROBOTS" content="INDEX, FOLLOW" />
<meta name="description" content="A music sharing community featuring free and legal music downloads, torrents and streaming audio." />
<meta name="keywords" content="music, sharing, streaming, creative commons, download, downloads, mp3, flac, audio, torrent, community" />
</asp:Content>
<asp:Content ContentPlaceHolderID="AdditionalScripts" runat="server">
	<%=Html.LibraryScript("jquery.ui") %>
	<%=Html.Script("jquery.jplayer.min.js") %>
	<%=Html.ScriptFromManifest("Home.Index") %>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="AdditionalStylesheets">
	<style type="text/css">
		.workListItemBoxWidth
		{
			width: 200px;
		}
	</style>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
	<%Html.RenderPartial("SearchPartial"); %>
	<h1>
		Free and Legal Music Downloads
	</h1>
	<div style="margin: 1.5em 0 3em">
		<div style="float: left; max-width: 70%; margin-right: 3em;">
			<div class="ui-corner-all" style="background-color: #e8eef4; padding: 2em 2em 2em 2em">
				<span style="font-weight: bold; font-size: 1.25em;">All music on this site can be freely downloaded and shared according to a <a href="http://creativecommons.org/" target="_blank">Creative Commons</a> Attribution license. </span>&nbsp;
				<%=Html.ActionLink("Read More...", "Index", "About" ) %>
			</div>
		</div>
		<div style="float: left;">
			<div class="ui-corner-all" style="background-color: #e8eef4; padding: 1em 1em 1em 1em; text-align: center">
				<div style="font-size: 1.25em; font-weight: bold; margin-bottom: .7em">
					Artists</div>
				<%=Html.IconLinkButton("Share your music", "Start Here", "ui-icon-circle-triangle-e", "ui-state-default", Url.Action("Register", "Account")) %>
				<div class="ui-helper-clearfix">
				</div>
			</div>
		</div>
		<div class="ui-helper-clearfix">
		</div>
	</div>
	<div>
		<div style="float: left; min-width: 26em; width: 50%">
			<div class="ui-widget-header ui-corner-all" style="padding: .7em; margin-right: 2em">
				Most Popular
			</div>
			<div style="padding:0 2em 0 0">
			<%Html.RenderPartial("WorkList", Model.MostPopular);  %>
			</div>
			
		</div>
		<div style="float: left; min-width: 26em; width: 50%">
			<div class="ui-widget-header ui-corner-all" style="padding: .7em;">
				New Releases
			</div>
			<%Html.RenderPartial("WorkList", Model.NewReleases);  %>
			
		</div>
		<div class="ui-helper-clearfix">
		</div>
	</div>
	<div id="downloadWindow" title="Download">
	</div>
	<div id="lightBox" title="Image">
	</div>
</asp:Content>
