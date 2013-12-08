<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MusicCompany.Website.Models.Comments.IndexViewModel>" %>
<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	<%=Html.FormatTitle("Comments") %>
</asp:Content>
<asp:Content ContentPlaceHolderID="HeadContent" runat="server">
	<meta name="ROBOTS" content="NOINDEX, NOFOLLOW" />
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">

    <h1>Comments</h1>
	<% Html.RenderPartial("CommentList", Model); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="AdditionalStylesheets" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="AdditionalScripts" runat="server">
</asp:Content>
