<%@ CONTROL language="C#" inherits="System.Web.Mvc.ViewUserControl<MusicCompany.Website.Models.Works.LicensesViewModel>" %>
<%foreach (LicenseDetailView license in Model.Licenses)
  { %>
<div style="margin-left: 100px; margin-right: auto; margin-top:1em; position: relative;">
	<a href="<%=license.Url %>" target="_blank" >
		<img style=" border:none; position: absolute; left: -100px" src="<%= license.ImageUrl %>" alt="<%=Html.Encode(license.Name) %>" />
	</a>
	<h3 style="padding-top:.5em">
		<%=Html.Encode(license.Name) %>
	</h3>
</div>
<div style="padding-top: 2em; padding-bottom: 2em">
	<%=Html.Encode(license.Description) %>
	<div>
		<a href="<%=license.Url %>" target="_blank">View License Deed</a>&nbsp;|&nbsp;<a href="<%=license.Url %>/legalcode" target="_blank">View Legal Code</a>
	</div>
</div>
<div class="hr">
	<hr />
</div>
<%} %>
<div style="margin-top: 3em">
	<%--TODO: attribute this properly--%>
	Source: <a href="http://creativecommons.org/about/licenses" target="_blank">http://creativecommons.org/about/licenses</a>
</div>
