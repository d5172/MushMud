<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MusicCompany.Website.Models.YourMusic.IndexViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Html.FormatTitle("Your Music") %>
</asp:Content>
<asp:Content ContentPlaceHolderID="AdditionalScripts" runat="server">
	<%=Html.ScriptFromManifest("YourMusic.Index") %>
	<script type="text/javascript">
		var global_WorkActivityUrl = '<%=Url.Action("WorkActivity","YourMusic") %>';
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h1>
		Your Music</h1>
	<div>
		<div style="float: left; min-width: 60%;">
			<div class="ui-widget-header ui-corner-all" style="padding: .7em; margin-right:2em">
				Your Artists
			</div>
			<% foreach (var artist in Model.Artists.OrderBy(a => a.Name))
	  { %>
			<div style="padding: .5em; ">
				<div style="float: left">
					<a href="<%=Url.Action("EditProfile", "Artist", new {artistId = artist.Identifier}) %>">
						<img src="<%=Url.Action("Artist", "Image", new {id = artist.ProfileImageId, width = 115})  %>" width="115"  style="width: 115; border: none" alt="Artist Picture: <%= Html.Encode(artist.Name) %>" title="Artist Picture: <%= Html.Encode(artist.Name) %>"/>
					</a>
				</div>
				<div style="float: left; padding-top: .5em; margin-left: 1em">
					<h3>
						<%= Html.ActionLink(artist.Name, "EditProfile", "Artist", new{artistId = artist.Identifier}, new {title="Edit Profile"})%>
					</h3>
					<div style="padding: .5em">
						<%=Html.DisplayCount("colection", artist.CollectionCount, true) %>
						|
						<%=Html.DisplayCount("single", artist.SingleCount, true) %>
					
					</div>
					<div>
						<%= Html.IconLinkButton("Edit Profile","Edit Profile", "ui-icon-person", "", Url.Action( "EditProfile", "Artist", new{artistId = artist.Identifier}))%>
						<%= Html.IconLinkButton("Manage Works","Create and edit songs and collections", "ui-icon-wrench", "", Url.Action( "Index", "Works", new{artistId = artist.Identifier}))%>
					</div>
				</div>
				<div class="ui-helper-clearfix">
				</div>
			</div>
			<%--<div class="hr" style="width:75%">
				<hr />
			</div>--%>
			<% } %>
			<div>
				<%=Html.IconLink("Create a new Artist Profile", "Create a New Artist Profile", "ui-icon-plusthick", "", Url.Action("Create", "Artist"))%>
			</div>
		</div>
		<div style="float: left; min-width: 15em; max-width: 40%;">
			<div class="ui-widget-header ui-corner-all" style="padding: .7em;">
				Recent Activity
			</div>
			
			<div id="activityBox" class="ui-corner-all" style="background-color: #e8eef4; padding:.5em; margin:.5em 0">
			</div>
		</div>
		<div class="ui-helper-clearfix">
		</div>
	</div>
</asp:Content>
