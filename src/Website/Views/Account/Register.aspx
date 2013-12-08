<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	<%=Html.FormatTitle("Create a new account")%>
</asp:Content>
<asp:Content ContentPlaceHolderID="AdditionalScripts" runat="server">
	<%=Html.LibraryScript("jquery.validate") %>
	<%=Html.LibraryScript("jquery.ui") %>
	<%--<script type="text/javascript" src="/content/captcha/jquery.captcha.js"></script>--%>

	<script type="text/javascript">
		var passwordLength = <%=ViewData["PasswordLength"]%>;
		var artistNameUrl = '<%=Url.Action("IsNameAvailable", "Artist")%>';
		var usernameUrl = '<%=Url.Action("IsUsernameAvailable", "Account")%>';
		var newArtistId = '<%=Guid.Empty %>';
		var captchaUrl = '<%=Url.Action("GetCaptcha", "Account")%>'
	</script>

	<%=Html.ScriptFromManifest("Account.Register")%>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="AdditionalStylesheets">
	<%--<link href="/content/captcha/captcha.css" rel="stylesheet" type="text/css" />--%>
</asp:Content>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
	<h1>
		Create a new account</h1>
	<p>
		Use the form below to create a new account.
	</p>
	<div>
		<form method="post" action="#" id="registrationForm" autocomplete="false">
		<ul>
			<li>
				<label for="username" class="desc">
					Username:</label>
				<div>
					<%= Html.TextBox("username", "", new{@id="txtUsername", title="Please enter a Username", @class="text"}) %>
				</div>
			</li>
			<li>
				<label for="email" class="desc">
					Email:</label>
				<div>
					<%= Html.TextBox("email", "", new{@id="txtEmail", title="Please enter an email address", @class="text"}) %>
				</div>
			</li>
			<li>
				<label for="password" class="desc">
					Password:</label>
				<div>
					<%= Html.Password("password", "", new{@id="txtPassword", title="Please enter a password", @class="text"}) %>
				</div>
			</li>
			<li>
				<label for="confirmPassword" class="desc">
					Confirm password:</label>
				<div>
					<%= Html.Password("confirmPassword", "", new{@id="txtConfirmPassword", title="Please confirm your password", @class="text"}) %>
				</div>
			</li>
			<li>
				<label for="artistName" class="desc">
					Band or Artist Name (optional):</label>
				<%= Html.TextBox("artistName", "", new{@id="txtArtistName", title="Enter the artist name", @class="text"})%>
			</li>
			<li>
				<%=Html.IconButton("Register", "ui-icon-check", "", "submit") %>
			</li>
		</ul>
		<%--<div style="width:300px; margin-bottom:1em">
			<div id="ajax-fc-container">
			</div>
		</div>--%>
		</form>
	</div>
</asp:Content>
