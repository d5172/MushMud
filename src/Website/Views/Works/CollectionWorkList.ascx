<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<MusicCompany.Common.ViewModel.CollectionSummaryView>>" %>
<%foreach (var item in Model)
	{ %>
<% Html.RenderPartial("CollectionWork", item); %>
<div class="hr"><hr /></div>
<%} %>
<div></div>