using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using MusicCompany.Website.Models;
using System.Configuration;
using System.IO;
using System.Text;
using MusicCompany.Common.ViewModel;
using System.Reflection;

namespace MusicCompany.Website.Extensions
{
	public static class HtmlHelperExtensions
	{
		private static readonly string buildKey = Assembly.GetExecutingAssembly().GetName().Version.Revision.ToString();
		
		#region Paging

		public static string PageLinks(this HtmlHelper html, int currentPage, int totalPages, string itemClass, string selectedClass, Func<int, string> pageUrl)
		{
			StringBuilder result = new StringBuilder();
			for ( int i = 1; i <= totalPages; i++ )
			{
				TagBuilder link = new TagBuilder("a");
				link.AddCssClass(itemClass);

				if ( i == currentPage )
				{
					link.AddCssClass(selectedClass);
				}
				if ( i != currentPage )
				{
					link.MergeAttribute("href", pageUrl(i));
				}
				link.InnerHtml = i.ToString();
				result.AppendLine(link.ToString());
			}
			return result.ToString();
		}

		#endregion

		#region UserMessage

		public static string InitialUserMessage(this HtmlHelper html)
		{
			var session = html.ViewContext.HttpContext.Session;
			var userMessage = session["UserMessage"] as UserMessage;
			if (userMessage != null)
			{
				session.Remove("UserMessage");
				return string.Format("<script type=\"text/javascript\">Shared.Common.InitialUserMessage = {0}Type:{1}, Message:'{2}'{3};</script>", "{", Convert.ToInt32(userMessage.Type), userMessage.Message, "}");	
			}
			return string.Empty;
		}

		#endregion

		#region Stylesheet helpers

		private static string CreateStylesheetLink(string url, string media)
		{
			return string.Format("<link rel=\"stylesheet\" href=\"{0}\" type=\"text/css\" media=\"{1}\" />", url, media);
		}

		public static string Stylesheet(this HtmlHelper html, string fileName)
		{
			UrlHelper helper = new UrlHelper(html.ViewContext.RequestContext);
			string url = helper.Content("~/Content/" + fileName);
			return CreateStylesheetLink(url, "screen");
		}

		public static string Stylesheet(this HtmlHelper html, string fileName, string media)
		{
			UrlHelper helper = new UrlHelper(html.ViewContext.RequestContext);
			string url = helper.Content("~/Content/" + fileName);
			return CreateStylesheetLink(url, media);
		}

		public static string JQueryUITheme(this HtmlHelper html)
		{
			return Stylesheet(html, ConfigurationManager.AppSettings["Theme"] + "/jquery-ui.css");
		}

		#endregion

		#region Script Tag helpers

		private static string CreateScriptTag(string url)
		{
			return string.Format("<script src=\"{0}\" type=\"text/javascript\"></script>", url);
		}

		private static string CreateScriptTagWithBuildKeyQuerystring(string url)
		{
			return string.Format("<script src=\"{0}?{1}\" type=\"text/javascript\"></script>", url, buildKey);
		}

		public static string Script(this HtmlHelper html, string fileName)
		{
			UrlHelper helper = new UrlHelper(html.ViewContext.RequestContext);
			string url = helper.Content("~/Scripts/" + fileName);
			return CreateScriptTagWithBuildKeyQuerystring(url);
		}

		/// <summary>
		/// Creates script tag(s) based on the specified manifest key
		/// </summary>
		public static string ScriptFromManifest(this HtmlHelper html, string key)
		{
#if DEBUG
            StringBuilder builder = new StringBuilder();
            string[] keyparts = key.Split('.');
            string folder = keyparts[0];
            string manifestFile = keyparts[1] + ".clientscriptmanifest";
            string virtualPath = "~/Views/" + folder + "/" + manifestFile;
            string manifestPath = html.ViewContext.HttpContext.Server.MapPath(virtualPath);
            string[] files = File.ReadAllLines(manifestPath);
            foreach ( string filePath in files )
            {
                    builder.AppendLine(CreateScriptTag("/"+filePath));
            }
            return builder.ToString();
#else
			return CreateScriptTagWithBuildKeyQuerystring("/Scripts/" + key + ".js");
#endif
		}

		public static string LibraryScript(this HtmlHelper html, string key)
		{
			return CreateScriptTag(ConfigurationManager.AppSettings[key]);
		}

		#endregion

		#region Display Formatters

		public static string FormatTitle(this HtmlHelper html, string title)
		{
			return html.Encode("MushMud.com " + title);
		}

		public static string FormatLicenseName(this HtmlHelper html, string licenseName)
		{
			return html.Encode(string.Format("Creative Commons {0} 3.0 License", licenseName));
		}

		public static string Truncate(this HtmlHelper html, string text, int preferredLength)
		{
			if (text.Length <= preferredLength * 1.5)
			{
				return html.Encode(text);
			}
			else
			{
				string output;
				string truncated = text.Substring(0, preferredLength);
				int lastBreak = truncated.LastIndexOfAny(new char[] { ' ', ',', '.' });
				if (lastBreak > 0)
				{
					output = truncated.Substring(0, lastBreak) + "...";
				}
				else
				{
					output = truncated;
				}
				return html.Encode(output);
			}
		}

		public static string DisplayCount(this HtmlHelper html, string entityName, int count, bool showZero)
		{
			if ( count > 0 || showZero )
			{
				return string.Format("{0} {1}{2}", count, entityName, count == 1 ? "" : "s");
			}
			else
			{
				return string.Empty;
			}
		}

		public static string DisplayCount(this HtmlHelper html, string entityName, int count)
		{
			return DisplayCount(html, entityName, count, true);
		}

		public static string DisplayStats(this HtmlHelper html, int downloadCount, int playCount)
		{
			string downloads = DisplayCount(html, "download", downloadCount, false);
			string plays = DisplayCount(html, "play", playCount, false);
			if (  playCount == 0 )
			{
				return downloads;
			}
			else if ( downloadCount == 0 )
			{
				return plays;
			}
			else
			{
				return downloads + " / " + plays;
			}
		}

		public static string DisplayDuration(this HtmlHelper html, TimeSpan duration)
		{
			if ( duration.Hours > 0 )
			{
				return string.Format("{0} hr, {1} min, {2} s", duration.Hours, duration.Minutes, duration.Seconds);
			}
			else
			{
				return string.Format("{0} minute{1}, {2} second{3}", duration.Minutes, duration.Minutes == 1 ? "": "s", duration.Seconds, duration.Seconds == 1 ? "" : "s");
			}
		}

		public static string DisplayDuration(this HtmlHelper html, int seconds)
		{
			return DisplayDuration(html, TimeSpan.FromSeconds(seconds));
		}

		public static string DisplayShortDuration(this HtmlHelper html, TimeSpan duration)
		{
			return string.Format("{0:D2}:{1:D2}", duration.Minutes, duration.Seconds);
		}

		public static string DisplayFileFormats(this HtmlHelper html, string primaryFormat, string alternateFormat)
		{
			if (!string.IsNullOrEmpty(alternateFormat))
			{
				return string.Format("{0}/{1}", primaryFormat, alternateFormat);
			}
			else 
			{
				return primaryFormat;
			}
		}

		public static string DisplayTags(this HtmlHelper html, string tagList, string label)
		{
			if ( string.IsNullOrEmpty(tagList) )
			{
				return string.Empty;
			}
			else
			{
				if ( string.IsNullOrEmpty(label) )
				{
					return tagList;
				}
				else
				{
					return string.Format("{0}{1}", label, tagList);
				}
			}
		}

		#endregion

		#region Jquery-UI helpers

		public static string IconButton(this HtmlHelper html, string text, string icon, string additionalClass, string id)
		{
			//TODO:  figure out why this this is like this and handle in a more standarized way
			string fireFoxFix;
			if ( html.ViewContext.RequestContext.HttpContext.Request.UserAgent.ToUpper().Contains("FIREFOX") )
			{
				fireFoxFix = "style=\"left:-1.5em\"";
			}
			else
			{
				fireFoxFix = "";
			}
			
			return string.Format("<button class=\"{2} fg-button fg-button-icon-left ui-state-default ui-corner-all\" id=\"{3}\"><span class=\"ui-icon {1}\" {4}></span>{0}</button>", text, icon, additionalClass, id, fireFoxFix);
		}

		public static string IconLabel(this HtmlHelper html, string text, string icon, string additionalClass)
		{
			return string.Format("<span class=\"mm-iconLink {2}\"><span class=\"ui-icon {1}\"></span>{0}</span>", text, icon, additionalClass);
		}

		public static string IconLink(this HtmlHelper html, string text, string title, string icon, string additionalClass, string route)
		{
			return string.Format("<a class=\"mm-iconLink {3}\" href=\"{4}\" title=\"{1}\"><span class=\"ui-icon {2}\"></span>{0}</a>", text, title, icon, additionalClass, route);
		}

		public static string IconLinkButton(this HtmlHelper html, string text, string title, string icon, string additionalClass, string route)
		{
			return string.Format("<a href=\"{0}\" class=\"{1} fg-button ui-state-default fg-button-icon-left ui-corner-all\" title=\"{2}\"><span class=\"ui-icon {3}\"></span>{4}</a>" ,route, additionalClass, title, icon, text);
		}

		public static string IconLinkButton(this HtmlHelper html, string text, string title, string icon, string additionalClass, string id, string route)
		{
			return string.Format("<a id=\"{5}\" href=\"{0}\" class=\"{1} fg-button ui-state-default fg-button-icon-left ui-corner-all\" title=\"{2}\"><span class=\"ui-icon {3}\"></span>{4}</a>" ,route, additionalClass, title, icon, text, id);
		}

		public static string IconLinkButton(this HtmlHelper html, string title, string icon, string additionalClass, string route)
		{
			return string.Format("<a href=\"{0}\" class=\"{1} fg-button ui-state-default fg-button-icon-solo ui-corner-all\" title=\"{2}\"><span class=\"ui-icon {3}\"></span></a>" ,route, additionalClass, title, icon);
		}

		#endregion

		#region DomainEventView Formatters

		public static string DisplayWho(this HtmlHelper html, DomainEventView domainEvent)
		{
			if ( string.IsNullOrEmpty(domainEvent.EventUsername) )
			{
				return "An anonymous user";
			}
			else if ( domainEvent.EventUsername.ToLowerInvariant() == html.ViewContext.HttpContext.User.Identity.Name.ToLowerInvariant() )
			{
				return "You";
			}
			else
			{
				return html.Encode(domainEvent.EventUsername);
			}
		}

		public static string DisplayWhat(this HtmlHelper html, DomainEventView domainEvent)
		{
			if ( domainEvent.DomainEventType == "Play" )
			{
				return "listened to";
			}
			else if ( domainEvent.DomainEventType == "Download" )
			{
				return "downloaded";
			}
			else if ( domainEvent.DomainEventType == "Comment" )
			{
				return "commented on";
			}
			else
			{
				return "viewed";
			}
		}

		public static string DisplayWhen(this HtmlHelper html, DomainEventView domainEvent)
		{
			return DisplayWhen(html, domainEvent.EventDate);
		}

		#endregion

		public static string DisplayWhen(this HtmlHelper html, DateTime eventDate)
		{
			if ( eventDate.DayOfYear == DateTime.Now.DayOfYear )
			{
				return string.Format("at {0:t}", eventDate);
			}
			else if ( eventDate.DayOfYear == DateTime.Now.AddDays(-1).DayOfYear )
			{
				return string.Format("yesterday at {0:t}", eventDate);
			}
			else if ( eventDate.DayOfYear >= DateTime.Now.AddDays(-6).DayOfYear )
			{
				return string.Format("on {0} at {1:t}", eventDate.DayOfWeek, eventDate);
			}
			else
			{
				return string.Format("on {0:d} at {0:t}", eventDate);
			}
		}
	}
}
