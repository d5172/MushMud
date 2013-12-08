<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%
	if ( Request.IsAuthenticated )
	{
%>
Hello, <b>
	<%= Html.Encode(Page.User.Identity.Name) %></b>
<%= Html.ActionLink("Log Off", "LogOff", "Account") %>
|
<%= Html.ActionLink("Account Settings", "Index", "Account") %>
<%
	}
	else
	{
%>
Welcome, <b>Guest</b>
<%= Html.ActionLink("Log On", "LogOn", "Account") %>
<%
	}
%>
