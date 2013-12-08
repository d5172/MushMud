<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MusicCompany.Website.Models.Music.TrackViewModel>" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	<%=Html.FormatTitle(Model.Track.Title) %>
</asp:Content>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
<meta name="ROBOTS" content="INDEX, FOLLOW" />
</asp:Content>
<asp:Content ContentPlaceHolderID="AdditionalStylesheets" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="AdditionalScripts" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=Html.Encode(Model.Track.Title) %></h2>

<div>
	<%Html.RenderPartial("TrackDetail", Model.Track); %>
</div>

</asp:Content>


