<%@ PAGE language="C#" masterpagefile="~/Views/Shared/Site.Master" inherits="System.Web.Mvc.ViewPage" %>

<ASP:CONTENT id="aboutTitle" contentplaceholderid="TitleContent" runat="server">
	About Us
</ASP:CONTENT>
<ASP:CONTENT contentplaceholderid="HeadContent" runat="server">
</ASP:CONTENT>
<ASP:CONTENT contentplaceholderid="AdditionalStylesheets" runat="server">
	<style type="text/css">
	</style>
</ASP:CONTENT>
<ASP:CONTENT contentplaceholderid="AdditionalScripts" runat="server">

	
	<script type="text/javascript">
		alert("is webkit: " + $.browser.webkit);
	</script>

</ASP:CONTENT>
<ASP:CONTENT id="aboutContent" contentplaceholderid="MainContent" runat="server">
	<h2>
		About</h2>
		
		
<a href="#">
<span class="ui-icon ui-icon-pencil" style="float:left; margin-right:.4em"></span>
	Edit the collection...
</a>
	
	<div>
		<a href="#" class="fg-button ui-state-default fg-button-icon-left ui-corner-all" title="Up"><span class="ui-icon ui-icon-arrowthick-1-n"></span>Up</a>
		
		<a href="#" class="fg-button ui-state-default fg-button-icon-left ui-corner-all" title="Edit"><span class="ui-icon ui-icon-pencil"></span>Edit</a>
		<a href="#" class="fg-button ui-state-default fg-button-icon-left ui-corner-all" title="Delete"><span class="ui-icon ui-icon-trash"></span>Delete</a>
		<a href="#" class="fg-button ui-state-default fg-button-icon-left ui-corner-all" title="Save"><span class="ui-icon ui-icon-check"></span>Save</a>
		<a href="#" class="fg-button ui-state-default fg-button-icon-left ui-corner-all" title="Cancel"><span class="ui-icon ui-icon-close"></span>Cancel</a>
		

		<div class="ui-helper-clearfix"></div>
	</div>
	<div style="margin-top:10px;">
		<a href="#" class="fg-button ui-state-default fg-button-icon-left ui-corner-all" title="Add Song"><span class="ui-icon ui-icon-plusthick"></span>Add Song</a>

		<a href="#" class="fg-button ui-state-disabled fg-button-icon-left ui-corner-all" 
		title="Select an image file..."><span class="ui-icon ui-icon-search"></span>Select audio files...</a>
		<div class="ui-helper-clearfix"></div>
	</div>
	
	<div style="margin-top:10px;">
		<a href="#" class="fg-button ui-state-default fg-button-icon-left ui-corner-all" title="Add Collection"><span class="ui-icon ui-icon-plusthick"></span>Add Collection</a>

		<div class="ui-helper-clearfix"></div>
	</div>
	<div style="margin-top:10px">
		<div style="float: left">
			1
		</div>
		<div style="float: left; height:16px">
			<a href="#" class="fg-button ui-state-default fg-button-icon-solo ui-corner-all"><span class="ui-icon ui-icon-play"></span></a>
		</div>
		<div style="float: left">
			Title
		</div>
		<div class="ui-helper-clearfix"></div>
		
	</div>
	<p>
		<a rel="license" href="http://creativecommons.org/licenses/by-sa/3.0/us/">
			<img alt="Creative Commons License" style="border-width: 0" src="http://i.creativecommons.org/l/by-sa/3.0/us/80x15.png" /></a><br />
		<span xmlns:dc="http://purl.org/dc/elements/1.1/" href="http://purl.org/dc/dcmitype/Sound" property="dc:title" rel="dc:type">XYZ</span> by <a xmlns:cc="http://creativecommons.org/ns#" href="[my url]" property="cc:attributionName" rel="cc:attributionURL">ABC</a> is licensed under a <a rel="license" href="http://creativecommons.org/licenses/by-sa/3.0/us/">Creative Commons Attribution-Share Alike 3.0 United States License</a>.
	</p>
</ASP:CONTENT>
