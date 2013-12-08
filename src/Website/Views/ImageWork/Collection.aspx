<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MusicCompany.Website.Models.ImageWork.CollectionImageViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Collection Image
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="AdditionalStylesheets" runat="server">
</asp:Content>

<asp:Content ID="Content5" ContentPlaceHolderID="AdditionalScripts" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=Model.CollectionTitle %></h2>
	<%Html.RenderPartial("CollectionImageList", Model);%>
</asp:Content>

