<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IPagedList<MusicCompany.Core.Work>>" %>
<div>
	<div style="font-weight: bold; margin-bottom:.5em">
		Works
		<%=Model.StartItem() %>-<%=Model.EndItem() %>
		of
		<%=Model.TotalItemCount %>
	</div>
	<div>
		<div class="pageLink ui-corner-all ui-state-default" style="float: left; margin-right: .5em; padding: .2em .8em .2em .8em">
			<a href="#" class="<%=Model.IsFirstPage ? "ui-state-disabled" : "" %>" title="First Page"><span class="ui-icon ui-icon-seek-first" style="float: right; margin-left: .1em"></span>First</a>
		</div>
		<div class="pageLink  ui-corner-all ui-state-default" style="float: left; margin-right: .5em; padding: .2em .8em .2em .8em">
			<a href="#" class="<%=Model.HasPreviousPage ? "" : "ui-state-disabled" %>" title="Previous Page"><span class="ui-icon ui-icon-seek-prev" style="float: right; margin-left: .1em"></span>Prev</a>
		</div>
		<div class="pageLink  ui-corner-all ui-state-default" style="float: left; margin-right: .5em; padding: .2em .8em .2em .8em">
			<a href="#" class="<%=Model.HasNextPage ? "" : "ui-state-disabled" %>" title="Next Page"><span class="ui-icon ui-icon-seek-next" style="float: left; margin-right: .1em"></span>Next</a>
		</div>
		<div class="pageLink  ui-corner-all ui-state-default" style="float: left; padding: .2em .8em .2em .8em">
			<a href="#" class="<%=Model.IsLastPage ? "ui-state-disabled" : "" %>" title="Last Page"><span class="ui-icon ui-icon-seek-end" style="float: left; margin-right: .1em"></span>Last</a>
		</div>
		<div class="ui-helper-clearfix">
		</div>
	</div>
</div>