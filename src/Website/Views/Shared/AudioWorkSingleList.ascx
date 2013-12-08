<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<MusicCompany.Common.ViewModel.AudioSingleSummaryView>>" %>
<%foreach (var item in Model)
	{ %>
<% Html.RenderPartial("AudioWorkSingle", item); %>
<%} %>