<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MusicCompany.Website.Models.AudioWork.AddToCollectionFormViewModel>" %>
<% using ( Html.BeginForm("AddToCollection", "AudioWork", new
   {
	   artistId = Model.ArtistId,
	   workId = Model.WorkId
   }, FormMethod.Post, new
   {
	   @class = "addToCollectionForm",
	   autocomplete = "off"
   }) )
   {  %>
<div style="margin-left: 2em">
	Add to
	<%=Html.DropDownList("CollectionIdentifier", Model.AvailableCollections, "Choose a collection...", new {id="ddlCollection"}) %>
	<%=Html.ActionLink("Cancel", "Index", "Works", new{artistId = Model.ArtistId}, new {@class="cancel"}) %>
</div>
<%} %>
