<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%=Html.FormatTitle("About")%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
<meta name="ROBOTS" content="INDEX, FOLLOW" />
<meta name="description" content="Describes the MushMud.com site, it's philisophy and services" />
<meta name="keywords" content="creative commons music" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="AdditionalStylesheets" runat="server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="AdditionalScripts" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h1>
		About MushMud.com</h1>
	
		
		<div style="font-size:1.3em; min-width:300px; max-width:500px; margin:0 3em 0 3em">
		<p>
			MushMud.com serves both artists and fans by providing free and legal music downloads.
			All works are licensed under a Creative Commons license, which allows for free file sharing and, depending on
			the specific license, the rights for remixing, sampling and use for commercial purposes, as long as the original
			artist is attributed.
			</p>
			<p>
			
			The artists of MushMud.com feel that their art should be freely available to anyone, and are more motived
			by getting their works out into the public without using a traditional record company rather than making
			money from recorded works.
		</p>
			
		</div>
		
</asp:Content>
