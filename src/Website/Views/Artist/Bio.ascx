<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<MusicCompany.Website.Models.Artist.ProfileViewModel>" %>

<%=Html.Encode(Model.Artist.Bio) %>