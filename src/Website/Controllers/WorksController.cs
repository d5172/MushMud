using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MusicCompany.Common.Commands;
using MusicCompany.Common.Queries;
using MusicCompany.Website.Models;
using MusicCompany.Website.Models.Works;

namespace MusicCompany.Website.Controllers
{
	[Authorize]
	public class WorksController : ExtendedController
	{

		private ActionResult ReturnToIndex(string artistId)
		{
			return RedirectToAction("Index", new{artistId = artistId});
		}

		/// <summary>
		/// Display artist's manage page (see all works with stats and crud links)
		/// </summary>
		public ActionResult Index(string artistId)
		{
			var model = new IndexViewModel();
			try
			{
				var artistQuery = new GetArtistWorkDetailQuery();
				artistQuery.Identifier = artistId;
				var artistResponse = this.ProcessRequest<GetArtistWorkDetailQueryResponse>(artistQuery);
				model.ArtistIdentifier = artistResponse.Artist.Identifier;
				model.ArtistName = artistResponse.Artist.Name;
				model.Collections = artistResponse.Artist.Collections;
				model.Singles = artistResponse.Artist.Singles;

				return GetResult(View(model),null);
			}
			catch (Exception ex)
			{
				return GetFailure(ex, GoHome());
			}
		}


		/// <summary>
		/// Renders a partial view of the collectionworks for editing
		/// </summary>
		/// <param name="artistId"></param>
		/// <returns></returns>
		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult CollectionWorks(string artistId, FormCollection form)
		{
			try
			{
				var collectionsQuery = new ListCollectionSummaryByArtistQuery();
				collectionsQuery.ArtistIdentifier = artistId;
				var collectionsResponse = this.ProcessRequest<ListCollectionSummaryByArtistQueryResponse>(collectionsQuery);

				return GetResult(null, View("CollectionWorkList", collectionsResponse.Collections));
			}
			catch (Exception ex)
			{
				return GetFailure(ex, this.ReturnToIndex(artistId));
			}
		}

		/// <summary>
		/// Processes the post to remove a work from a collection
		/// </summary>
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult RemoveFromParent(string artistId, string workId, string collectionId, FormCollection formCollection)
		{
			try
			{
				var command = new RemoveWorkFromCollectionCommand();
				command.ArtistIdentifier = artistId;
				command.CollectionIdentifier = collectionId;
				command.WorkIdentifier = workId;
				var response = this.ProcessRequest<RemoveWorkFromCollectionCommandResponse>(command);

				UserMessage message = new UserMessage(string.Format("{0} was removed from {1}", response.WorkTitle, response.CollectionTitle), UserMessageType.Info);
				return PostResult(message, this.ReturnToIndex(artistId));
			}
			catch (Exception ex)
			{
				return PostFailure(ex, this.ReturnToIndex(artistId));
			}
		}

		/// <summary>
		/// Processes the post to delete a single work
		/// </summary>
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Delete(string artistId, string workId, FormCollection formCollection)
		{
			try
			{
				var command = new DeleteWorkCommand();
				command.ArtistIdentifier = artistId;
				command.WorkIdentifier = workId;

				var response = this.ProcessRequest<DeleteWorkCommandResponse>(command);

				UserMessage message = new UserMessage(response.WorkTitle + " was deleted", UserMessageType.Info);
				return PostResult(message, this.ReturnToIndex(artistId));
			}
			catch (Exception ex)
			{
				return PostFailure(ex, this.ReturnToIndex(artistId));
			}
		}

		/// <summary>
		/// Processes the post to sort child works
		/// </summary>
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult SortChildWorksByIds(string artistId, string workId, FormCollection formCollection)
		{
			try
			{
				var command = new SortChildWorksCommand();
				command.ArtistIdentifier = artistId;
				command.CollectionIdentifier = workId;

				string ids = formCollection["ids"];
				List<string> workIds = new List<string>();
				foreach (string id in ids.Split(new char[]{';'}, StringSplitOptions.RemoveEmptyEntries))
				{
					workIds.Add(id);
				
				}
				command.SortedIdentifiers = workIds;

				var response = this.ProcessRequest<SortChildWorksCommandResponse>(command);

				UserMessage message = new UserMessage(response.CollectionTitle + " was sorted", UserMessageType.Info);
				return PostResult(message, this.ReturnToIndex(artistId));
			}
			catch (Exception ex)
			{
				return PostFailure(ex, this.ReturnToIndex(artistId));
			}
		}
		
		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult SuggestTags(string q)
		{
			var query = new SuggestTagsQuery();
			query.StartingWith = q;
			var response = this.ProcessRequest<SuggestTagsQueryResponse>(query);
			var tags = response.SuggestedTags;
			string plainTextContentResult;
			if (tags.Length > 0)
			{
				plainTextContentResult = string.Join(Environment.NewLine, tags);
			}
			else
			{
				plainTextContentResult = " ";	//Firefox complains about no element found if empty string
			}
			return Content(plainTextContentResult, "text/plain");
		}

		
		[AcceptVerbs(HttpVerbs.Get)]
		[OutputCache(Duration=20, VaryByParam="*")]
		public ActionResult Licenses()
		{
			var query = new ListLicenseDetailQuery();
			var response = this.ProcessRequest<ListLicenseDetailQueryResponse>(query);
			var model = new LicensesViewModel();
			model.Licenses = response.Licenses;
			return GetResult(View(model), View("LicenseList", model));	
		}

		
		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult LicenseChooser()
		{
			return View("LicenseChooser");			
		}
	}
}