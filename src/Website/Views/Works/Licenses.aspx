<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" 
Inherits="System.Web.Mvc.ViewPage<MusicCompany.Website.Models.Works.LicensesViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Licenses
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Licenses</h1>
<%Html.RenderPartial("LicenseList", Model);%>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="AdditionalStylesheets" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="AdditionalScripts" runat="server">
</asp:Content>
