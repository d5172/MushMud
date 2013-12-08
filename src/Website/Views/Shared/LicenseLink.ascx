<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MusicCompany.Common.ViewModel.LicenseDetailView>" %>
<div style="font-size: 0.8em">
	<a rel="license" href="<%=Model.Url%>" target="_blank">
		<%=Html.FormatLicenseName(Model.Name )%></a>.
</div>
