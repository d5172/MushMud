<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MusicCompany.Website.Models.Comments.CommentFormViewModel>" %>
<% using ( Html.BeginForm("Add", "Comments", new
   {
	   workId = Model.Command.WorkId
   }, FormMethod.Post, new
   {
	   @class = "commentForm",
	   autocomplete = "off"
   }) )
   {  %>
<ul>
	<li>
		<label for="Title" class="desc">
			Your Comment:</label>
		<div>
			<%= Html.TextArea("CommentText", Model.Command.CommentText, new{@id="txtComments", title="Please enter some comments", @class="text", rows=4, style="width:320px;"}) %>
		</div>
	</li>
	<li>
		<div class="commands">
			<%=Html.IconButton("Save", "ui-icon-check", "", "submit") %>
			or
			<%=Html.ActionLink("Cancel", "Index", "Comments", new{workId= Model.Command.WorkId}, new {@class="cancel"}) %>
		</div>
	</li>
</ul>
<%} %>
