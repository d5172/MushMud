<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MusicCompany.Website.Models.YourMusic.WorkActivityViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Html.FormatTitle("Recent Activity") %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Recent Activity</h1>
    
    <%Html.RenderPartial("WorkActivityList", Model); %>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="AdditionalStylesheets" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="AdditionalScripts" runat="server">
</asp:Content>
