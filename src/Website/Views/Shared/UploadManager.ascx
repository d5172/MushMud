<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="uploadWindow" class="ui-helper-hidden" title="Upload">
	<p>
	</p>
	<div class="flashBox">
	</div>
	<div id="fileQueue" class="ui-helper-hidden">
		<span id="fileQueueLabel"></span>
		<div class="ui-corner-all" style="background-color: #e8eef4;">
			<table id="fileQueueList">
			</table>
		</div>
		<div id="uploadProgress" style="height: .8em">
		</div>
		<div class="disclaimer" style="padding: 1em 0 1em 0">
			By uploading files, I assume all responsibility for copyright violations resulting
			from making this file available, and have the legal rights to assign the chosen
			license.
		</div>
		<div class="commands" style="text-align: center">
			<%=Html.IconLinkButton("Agree and Upload", "Agree and Upload", "ui-icon-check", "", "btnUpload", "#") %>
			<div id="uploadThrobber">
			</div>
		</div>
	</div>
</div>