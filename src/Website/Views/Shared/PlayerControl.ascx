<%@ CONTROL language="C#" inherits="System.Web.Mvc.ViewUserControl<MusicCompany.Core.AudioWork>" %>
<div id="pl<%=Model.Id.ToString() %>"></div>
<script type="text/javascript">
	var so = new SWFObject('<%=Url.Content("~/Content/Player.swf")%>', "swf", "100%", "50", "9,0,115,0", "#ffffff");
	so.addParam("allowScriptAccess", "always");
	so.addParam("scale", "noscale");
	so.addParam("menu", "false");
	so.addVariable("streamId", "<%=Model.File.Id.ToString() %>");
	so.addVariable("workTitle", "<%=Html.Encode(Model.Title) %>");
	so.addVariable("workDuration", "<%=Model.File.Seconds.ToString() %>");
	//so.addVariable("enableLog", "true");
	so.write("pl<%=Model.Id.ToString() %>");
</script>