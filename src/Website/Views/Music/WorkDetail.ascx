<%@ CONTROL language="C#" inherits="System.Web.Mvc.ViewUserControl<MusicCompany.Core.Work>" %>
<div>
	<%=Html.Encode(Model.Description) %>
</div>
<div>
	<%=Html.DisplayTags(Model, "Tags:") %>
</div>
