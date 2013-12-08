<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<%Html.BeginForm("Search", "Music", FormMethod.Get); %>
<div style="float: right">
	<div style="float: left; padding:.3em .75em;">
		<input id="txtSearch" name="terms" value="<%=Session["searchTerms"] %>" />
	</div>
	<div style="float: left;">
		<%=Html.IconLinkButton("Search", "Search", "ui-icon-search", "", "lnkSearch", "#")%>
	</div>
	<div class="ui-helper-clearfix">
	</div>
</div>
<%Html.EndForm();%>