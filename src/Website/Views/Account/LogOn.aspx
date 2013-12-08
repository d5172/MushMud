<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	<%=Html.FormatTitle("Log On")%>
</asp:Content>
<asp:Content ContentPlaceHolderID="AdditionalScripts" runat="server">
	<%=Html.LibraryScript("jquery.ui") %>
	<%=Html.ScriptFromManifest("Account.LogOn")%>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
	<h1>
		Log On</h1>
	<div>
		<% Html.RenderPartial("LogOnForm"); %>
	</div>
</asp:Content>
