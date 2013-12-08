<%@ CONTROL language="C#" inherits="System.Web.Mvc.ViewUserControl<MusicCompany.Common.ViewModel.TopLevelWorkSummaryView>" %>
<div  style="font-size:0.8em">
	<a rel="license" href="<%=Model.License.Url%>" title="<%=Html.Encode(Model.License.Name) %>" target="_blank">
		<img alt="Creative Commons License" style="border-width: 0" src="<%= Model.License.ImageUrl %>" /></a>
	<br />
	<span xmlns:dc="http://purl.org/dc/elements/1.1/" href="http://purl.org/dc/dcmitype/Sound" property="dc:title" rel="dc:type">
		<%=Html.Encode(Model.Title) %></span> by <a xmlns:cc="http://creativecommons.org/ns#" href="<%= Url.Action("Profile", "Artist", new{artistId= Model.ArtistIdentifier})%>" property="cc:attributionName" rel="cc:attributionURL">
			<%=Html.Encode(Model.ArtistName) %></a> is licensed under a  
			<a rel="license" href="<%=Model.License.Url%>" target="_blank"> <%=Html.FormatLicenseName(Model.License.Name )%></a>.
</div>
