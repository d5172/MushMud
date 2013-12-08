<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<MusicCompany.Common.ViewModel.TopLevelWorkSummaryView>>" %>
<% foreach ( var item in Model )
   {
%>
<div style="margin: 1em 0 1em;">
	<%
		if ( item is MusicCompany.Common.ViewModel.CollectionSummaryView )
		{
			Html.RenderPartial("WorkListCollectionItem", item);
		}
		else if ( item is MusicCompany.Common.ViewModel.AudioSingleSummaryView )
		{
			Html.RenderPartial("WorkListAudioItem", item);
		}
	%>
	<div class="workListItemBoxWidth" style=" padding: .2em 0 0 .5em; margin: 0 1em 1em 1em">
	<%=Html.IconLink("Comments", "View the comments", "ui-icon-comment", "expandComm", Url.Action("Index", "Comments", new {id=item.Id})) %>
	<div style="width:400px" class="commBox ui-helper-hidden">
	</div>
</div>
	<div class="hr">
		<hr />
	</div>
</div>
<%
	} %>