<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MusicCompany.Website.Models.Music.DownloadFormViewModel>" %>
<div>
	<div style="margin-top: 1em">
		You have chosen to download <span xmlns:dc="http://purl.org/dc/elements/1.1/" href="http://purl.org/dc/dcmitype/Sound" property="dc:title" rel="dc:type"><b>
			<%=Html.Encode(Model.WorkTitle) %></b></span> by <a xmlns:cc="http://creativecommons.org/ns#" href="<%= Url.Action("Profile", "Artist", new{artistId= Model.ArtistIdentifier})%>" property="cc:attributionName" rel="cc:attributionURL">
				<%=Html.Encode(Model.ArtistName) %></a>.
	</div>
	<div style="margin-top: 1em; margin-bottom: 1em;">
		<h2>
			To download the file(s) you must agree to the following license:</h2>
		<div class="ui-corner-all" style="text-align: center; background-color: #e8eef4; padding: 2em 2em 2em 2em; margin: 1em 0 2em">
			<a rel="license" href="<%=Model.License.Url%>" title="<%=Html.Encode(Model.License.Name) %>" target="_blank">
				<img alt="Creative Commons License" style="border-width: 0" src="<%= Model.License.ImageUrl %>" /></a>
			<br />
			<h3>
				<a rel="license" href="<%=Model.License.Url%>" target="_blank">
					<%=Html.FormatLicenseName(Model.License.Name )%></a>.
			</h3>
			<a rel="license" href="<%=Model.License.Url%>" target="_blank">Read the summary</a> | <a rel="license" href="<%=Model.License.Url%>/legalcode" target="_blank">View the full legal code</a>
		</div>
	</div>
</div>
<div style="text-align: center">
	<form method="post" action="<%=Model.FormAction%>">
	<%=Html.IconButton("Agree and Download " + Model.FileFormat + " File", "ui-icon-check", "confirmDownloadButton", "btnDownload") %>
	</form>
	<%if ( !string.IsNullOrEmpty(Model.AlternateFormAction) )
   { %>
	<form method="post" action="<%=Model.AlternateFormAction%>">
	<%=Html.IconButton("Agree and Download " + Model.AlternateFileFormat + " File", "ui-icon-check", "confirmDownloadButton", "btnDownloadAlt") %>
	</form>
	<%} %>
</div>
