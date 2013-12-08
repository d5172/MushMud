<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MusicCompany.Website.Models.UserMessage>" %>
<span class="<%=Model.Type %>"><%=Model.Message %></span>