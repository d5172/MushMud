<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MusicCompany.Website.Models.Comments.IndexViewModel>" %>
<div style="margin: 1em .2em 1em;" class="addBox">
	<%=Html.IconLink("Add a comment", "click here to add your own comment", "ui-icon-pencil", "addComment", Url.Action("Add", "Comments", new {workId=Model.WorkId}))  %>
</div>
<div class="formBox" style="padding: 0 1em;">
</div>
<div>
	<%foreach ( var comment in Model.Comments )
   { %>
	<div style="margin-bottom: 1em; border: solid 1px  #e8eef4; padding: .5em 0 .5em" class="comment ui-corner-all">
		<%--<%=Html.IconLink(comment.Username, "View " + comment.Username + "'s profile", "ui-icon-person", "", Url.Action("Profile", "Person", new{username=comment.Username}))  %>--%>
		<b>
			<%=Html.IconLabel(comment.Username, "ui-icon-person", "") %></b>
		<%=Html.DisplayWhen(comment.DateEntered) %>
		<div style="margin: .5em 1.75em;">
			<%=comment.CommentText %>
			<%--<br /><%=Html.IconLink("Reply", "click here to reply to this comment", "ui-icon-pencil", "replyComment", Url.Action("Add", "Comments", new {workId=Model.WorkId, commentId = comment.Id})) %>--%>
		</div>
	</div>
	<%} %>
</div>
<div>
	<div style="float: left;">
		<a href="<%= Model.Comments.HasPreviousPage ? Url.Action("Index", "Comments", new {id=Model.WorkId, page=Model.Comments.PageNumber-1}) : "" %>" class="pageLink <%=Model.Comments.HasPreviousPage ? "" : "ui-state-disabled ui-helper-hidden" %>" title="Previous Page"><span class="ui-icon ui-icon-seek-prev" style="float: left; margin-right: .1em"></span>Newer</a>
	</div>
	<div style="float: right;">
		<a href="<%=Model.Comments.HasNextPage ? Url.Action("Index", "Comments", new {id=Model.WorkId, page=Model.Comments.PageNumber+1}) : "" %>" class="pageLink <%=Model.Comments.HasNextPage ? "" : "ui-state-disabled ui-helper-hidden" %>" title="Next Page"><span class="ui-icon ui-icon-seek-next" style="float: right; margin-left: .1em"></span>Older Comments</a>
	</div>
	<div class="ui-helper-clearfix">
	</div>
</div>
