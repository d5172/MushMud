<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MusicCompany.Website.Models.CollectionWorkDTO>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Delete Collection
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h2>
		Delete Collection</h2>
	Are you sure you want to delete
	<%= Html.Encode(Model.Title) %>?
	<p>This will delete all the songs in the collection, too!</p>
	<div>
		<%using (Html.BeginForm())
	  { %>
		<input id="confirmButton" name="confirmButton" type="submit" value="Yes, Delete" />
		<%} %>
	</div>
	<div>
		<%=Html.ActionLink("Cancel", "Index", "Works", new{artistId = Model.ArtistId}, null)%>
	</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
