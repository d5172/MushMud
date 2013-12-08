<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MusicCompany.Website.Models.Music.DownloadFormViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Html.FormatTitle("Download: " + Model.WorkTitle + " by " + Model.ArtistName) %>
</asp:Content>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
<meta name="ROBOTS" content="INDEX, FOLLOW" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="AdditionalStylesheets" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="AdditionalScripts" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Download
    <%=Html.Encode(Model.WorkTitle) %>
    by
    <%=Html.Encode(Model.ArtistName) %></h2>
    
    <div>
    <%Html.RenderPartial("DownloadForm", Model); %>
    </div>

</asp:Content>