<%@Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="changePasswordTitle" ContentPlaceHolderID="TitleContent" runat="server">
   <%=Html.FormatTitle("Change your password")%>
</asp:Content>
<ASP:CONTENT contentplaceholderid="AdditionalScripts" runat="server">
	<%=Html.ScriptFromManifest("Account.ChangePasswordSuccess")%>
</ASP:CONTENT>
<asp:Content ID="changePasswordSuccessContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Change your password</h2>
        Your password has been changed successfully.
    </p>
</asp:Content>
