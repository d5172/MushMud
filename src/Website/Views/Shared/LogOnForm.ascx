<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MusicCompany.Website.Models.Account.LogOnFormViewModel>" %>
<div style="padding:.5em 0 .5em 0">
	Please enter your username and password.<br />
	<%= Html.ActionLink("Register", "Register", "Account") %> if you don't have an account.
</div>
<%using ( Html.BeginForm("LogOn", "Account", null, FormMethod.Post, new
  {
	  autocomplete = "false"
  }) )
  { %>
<ul>
	<li>
		<label for="username" class="desc">
			Username:</label>
		<div>
			<%= Html.TextBox("username") %>
		</div>
	</li>
	<li>
		<label for="password" class="desc">
			Password:</label>
		<div>
			<%= Html.Password("password") %>
		</div>
	</li>
	<li>
		<%= Html.CheckBox("rememberMe") %>
		<label class="inline" for="rememberMe">
			Remember me</label>
	</li>
	<li>
		<%=Html.IconButton("Log On", "ui-icon-check", "", "submit") %>
		or
		<%=Html.ActionLink("Cancel", "Index", "Home", null, new {@class="cancel"}) %>
	</li>
</ul>
<%} %>
