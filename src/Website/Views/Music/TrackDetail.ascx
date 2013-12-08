<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MusicCompany.Common.ViewModel.AudioTrackSummaryView>" %>
<div>
	<%=Html.Encode(Model.Description) %>
</div>
<div>
	<%=Html.DisplayTags(Model.Tags, "Tags:") %>
</div>