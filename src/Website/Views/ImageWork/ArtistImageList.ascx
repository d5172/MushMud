<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MusicCompany.Website.Models.ImageWork.ArtistImageViewModel>" %>
<div style="text-align: center">
	<%foreach ( var image in Model.Images )
   { %>
	<img src="<%=Url.Action("Artist", "Image", new {id = image.BinaryFileId, width=400})  %>" width="400" style="width: 400; border: none" alt="Artist Picture: <%= Html.Encode(image.Title) %>" title="Artist Picture: <%= Html.Encode(image.Title) %>" />
		<% if(!image.BinaryFileId.Equals(Guid.Empty)){ %>
		<div style="text-align:left; margin:0 0 0 .5em">
			<%=Html.ActionLink("Download a higher resolution image", "FullSize", "Image",new {id = image.BinaryFileId, title=image.Title}, new {rel="nofollow"})  %>
		</div>
		<%} %>
	<%} %>
</div>
