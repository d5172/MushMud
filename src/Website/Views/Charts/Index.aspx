<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MusicCompany.Website.Models.Charts.IndexViewModel>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	<%=Html.FormatTitle("Charts") %>
</asp:Content>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
<meta name="ROBOTS" content="INDEX, FOLLOW" />
<meta name="description" content="Lists the most popular songs and collections on MushMud.com" />
<meta name="keywords" content="popular downloads mp3 flac audio torrent" />
</asp:Content>
<asp:Content ContentPlaceHolderID="AdditionalStylesheets" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="AdditionalScripts" runat="server">
<%=Html.ScriptFromManifest("Charts.Index") %>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<%Html.RenderPartial("SearchPartial"); %>
	<h1>
		MushMud Charts</h1>
	<div style="margin-top: 1.5em; padding: 0 1em 0 1em; min-width:400px; max-width:900px">
		<h2>
			The Top 100</h2>
		<div style="background-color: #e8eef4; padding:.5em" class="ui-corner-all">
			<table cellpadding="5" width="100%">
				<thead  style="font-weight:bold">
					<tr>
						<td>
							Rank
						</td>
						<td>
							Title
						</td>
						<td>
							Artist
						</td>
						<td>
							Stats
						</td>
					</tr>
				</thead>
				<%foreach ( var work in Model.Works )
	  {
		  bool isSingle = (work is MusicCompany.Common.ViewModel.AudioSingleSummaryView); %>
				<tr>
					<td style="white-space: nowrap;">
						<span style="font-weight: bold">#
							<%=work.Rank %>
						</span>
					</td>
					<td width="100%">
						<%=Html.ActionLink(work.Title, isSingle ? "Single" : "Collection", "Music", new {artistId = work.ArtistIdentifier, workId = work.Identifier}, null) %>
					</td>
					<td style="white-space: nowrap;">
						<%=Html.ActionLink(work.ArtistName, "Profile", "Artist", new {artistId = work.ArtistIdentifier}, null) %>
					</td>
					<td style="white-space: nowrap;">
						<%=Html.DisplayStats(work.DownloadCount, work.PlayCount) %>
					</td>
				</tr>
				<%} %>
			</table>
		</div>
	</div>
</asp:Content>
