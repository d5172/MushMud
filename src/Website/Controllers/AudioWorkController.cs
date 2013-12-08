using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicCompany.Common.Commands;
using MusicCompany.Common.Queries;
using MusicCompany.Website.Models;
using MusicCompany.Website.Models.AudioWork;

namespace MusicCompany.Website.Controllers
{
	[Authorize]
	public class AudioWorkController : ExtendedController
	{
		#region Private Methods

		private ActionResult ReturnToWorks(string artistId)
		{
			return RedirectToAction("Index", "Works", new
			{
				artistId = artistId
			});
		}

		#endregion

		#region Action Methods

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult SuggestTags(string q)
		{
			var query = new SuggestTagsQuery();
			query.StartingWith = q;
			var response = this.ProcessRequest<SuggestTagsQueryResponse>(query);
			return Content(string.Join(Environment.NewLine, response.SuggestedTags), "text");
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult AddToCollection(string artistId, string workId)
		{
			try
			{
				var model = new AddToCollectionFormViewModel();

				var query = new ListCollectionSummaryByArtistQuery();
				query.ArtistIdentifier = artistId;
				var response = this.ProcessRequest<ListCollectionSummaryByArtistQueryResponse>(query);
				model.AvailableCollections = new SelectList(response.Collections, "Identifier", "Title");

				model.ArtistId = artistId;
				model.WorkId = workId;

				return this.GetResult(View(model), View("AddToCollectionForm", model));
			}
			catch ( Exception ex )
			{
				return GetFailure(ex, this.ReturnToWorks(artistId));
			}
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult AddToCollection(string artistId, string workId, AddSingleToCollectionCommand command)
		{
			try
			{
				command.ArtistIdentifier = artistId;
				command.WorkIdentifier = workId; 

				var response = this.ProcessRequest<AddSingleToCollectionCommandResponse>(command);

				UserMessage message = new UserMessage(string.Format("{0} was moved to {1}", response.WorkTitle, response.CollectionTitle), UserMessageType.Info);
				return PostResult(message, this.ReturnToWorks(artistId));

			}
			catch ( Exception ex )
			{
				return this.PostFailure(ex, this.ReturnToWorks(artistId));
			}
		}

		/// <summary>
		/// Processes the upload song form post
		/// </summary>
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult CreateInCollection(string artistId, string workId, FormCollection formCollection)
		{
			HttpPostedFileBase postedFile = null;
			try
			{
				postedFile = Request.Files[0];

				CreateTrackCommand command = new CreateTrackCommand();
				command.ArtistIdentifier = artistId;
				command.CollectionIdentifier = workId;
				command.FileName = postedFile.FileName;
				command.InputStream = postedFile.InputStream;

				var response = this.ProcessRequest<CreateTrackCommandResponse>(command);
				
				UserMessage message = new UserMessage(string.Format("{0} was created", response.Title), UserMessageType.Info);

				if (formCollection.AllKeys.Contains("ajax"))
				{
					return Json(message);
				}
				else
				{
					return this.PostResult(message, this.ReturnToWorks(artistId));
				}
			}
			catch (Exception ex)
			{
				if (formCollection.AllKeys.Contains("ajax"))
				{
					UserMessage userMessage = this.GetExceptionUserMessage(ex);
					if (postedFile != null)
					{
						userMessage.Message = string.Format("{0} could not be processed: {1}", postedFile.FileName, userMessage.Message);
					}
					return Json(userMessage);
				}
				else
				{
					return this.PostFailure(ex, this.ReturnToWorks(artistId));
				}
			}
		}

		/// <summary>
		/// Processes the upload song form post
		/// </summary>
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult CreateSingle(string artistId, FormCollection formCollection)
		{
			HttpPostedFileBase postedFile = null;
			try
			{
				postedFile = Request.Files[0];
				CreateAudioSingleCommand command = new CreateAudioSingleCommand();
				command.License = formCollection["License"];
				command.FileName = postedFile.FileName;
				command.InputStream = postedFile.InputStream;
				command.ArtistIdentifier = artistId;

				var response = this.ProcessRequest<CreateAudioSingleCommandResponse>(command);
				
				UserMessage message = new UserMessage(string.Format("{0} was created", response.Title), UserMessageType.Info);

				if (formCollection.AllKeys.Contains("ajax"))
				{
					return Json(message);
				}
				else
				{
					return this.PostResult(message, this.ReturnToWorks(artistId));
				}
			}
			catch (Exception ex)
			{
				if (formCollection.AllKeys.Contains("ajax"))
				{
					UserMessage userMessage = this.GetExceptionUserMessage(ex);
					if (postedFile != null)
					{
						userMessage.Message = string.Format("{0} could not be processed: {1}", postedFile.FileName, userMessage.Message);
					}
					return Json(userMessage);
				}
				else
				{
					return this.PostFailure(ex, this.ReturnToWorks(artistId));
				}
			}
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult EditTrack(string artistId, string workId, string collectionId)
		{
			try
			{
				var request = new GetTrackDetailQuery();
				request.ArtistIdentifier = artistId;
				request.CollectionIdentifier = collectionId;
				request.WorkIdentifier = workId;
				var response = this.ProcessRequest<GetTrackDetailQueryResponse>(request);
				var track = response.Track;

				var command = new UpdateTrackCommand();
				command.ArtistIdentifier = artistId;
				command.CollectionIdentifier = collectionId;
				command.WorkIdentifier = workId;
				command.Description = track.Description;
				command.Tags = track.Tags;
				command.Title = track.Title;

				var model = new EditTrackViewModel();
				model.Command = command;
				
				return this.GetResult(View(model), View("TrackForm", model));
			}
			catch (Exception ex)
			{
				return GetFailure(ex, this.ReturnToWorks(artistId));
			}
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult EditTrack(string artistId, string workId, string collectionId, UpdateTrackCommand command)
		{
			try
			{
				command.ArtistIdentifier = artistId;
				command.WorkIdentifier = workId;
				command.CollectionIdentifier = collectionId;
				var response = this.ProcessRequest<UpdateTrackCommandResponse>(command);
				UserMessage message = new UserMessage(string.Format("{0} was updated", response.Title), UserMessageType.Info);
				return PostResult(message, this.ReturnToWorks(artistId));

			}
			catch (Exception ex)
			{
				return this.PostFailure(ex, this.ReturnToWorks(artistId));
			}
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult EditSingle(string artistId, string workId)
		{
			try
			{
				var audioSingleDetailQuery = new GetAudioSingleDetailQuery();
				audioSingleDetailQuery.ArtistIdentifier = artistId;
				audioSingleDetailQuery.WorkIdentifier = workId;
				var queryResponse = this.ProcessRequest<GetAudioSingleDetailQueryResponse>(audioSingleDetailQuery);
				var single = queryResponse.Single;

				var command = new UpdateAudioSingleCommand();
				command.ArtistIdentifier = artistId;
				command.WorkIdentifier = workId;
				command.Description = single.Description;
				command.Tags = single.Tags;
				command.Title = single.Title;
				command.LicenseIdentifier = single.License.Identifier;
				command.ReleaseDate = single.ReleaseDate;

				var model = new EditSingleViewModel();
				model.Command = command;

				model.AvailableLicenses = new SelectList(this.GetLicenseSummaries(), "Identifier", "Name", single.License.Identifier);

				return this.GetResult(View(model), View("SingleForm", model));
			}
			catch ( Exception ex )
			{
				return GetFailure(ex, this.ReturnToWorks(artistId));
			}
		}

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult EditSingle(string artistId, string workId, UpdateAudioSingleCommand command)
		{
			try
			{
				command.ArtistIdentifier = artistId;
				command.WorkIdentifier = workId;
				var response = this.ProcessRequest<UpdateAudioSingleCommandResponse>(command);
				UserMessage message = new UserMessage(response.Title + " was updated", UserMessageType.Info);
				return PostResult(message, this.ReturnToWorks(artistId));

			}
			catch ( Exception ex )
			{
				return this.PostFailure(ex, this.ReturnToWorks(artistId));
			}
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult ListInCollection(string artistId, string workId)
		{
			try
			{
				var query = new ListTracksQuery();
				query.ArtistIdentifier = artistId;
				query.CollectionIdentifier = workId;
				var response = this.ProcessRequest<ListTracksQueryResponse>(query);
				var model = response.Tracks;
				return GetResult(null, View("AudioWorkList", model));
			}
			catch (Exception ex)
			{
				return GetFailure(ex, null);
			}
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult ListSingles(string artistId)
		{
			try
			{
				var query = new ListAudioSinglesQuery();
				query.ArtistIdentifier = artistId;
				var response = this.ProcessRequest<ListAudioSinglesQueryResponse>(query);
				var model = response.Singles;
				return GetResult(null, View("AudioWorkSingleList", model));
			}
			catch (Exception ex)
			{
				return GetFailure(ex, null);
			}
		}

		#endregion
	}
}