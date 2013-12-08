using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicCompany.Common.Commands;
using MusicCompany.Common.Queries;
using MusicCompany.Website.Models;
using MusicCompany.Website.Models.CollectionWork;

namespace MusicCompany.Website.Controllers
{
	[Authorize]
	public class CollectionWorkController : ExtendedController
	{

		public int ImageWidth = 115;

		#region Private Methods

		private ActionResult ReturnToWorks(string artistId)
		{
			return RedirectToAction("Index", "Works", new
			{
				artistId = artistId
			});
		}

		#endregion

		#region  Create
		/// <summary>
		/// display create collection work form
		/// </summary>
		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Create(string artistId)
		{
			try
			{
				var model = new CreateViewModel();
				model.Form = new CollectionWorkFormViewModel();
				model.Form.AvailableLicenses = new SelectList(this.GetLicenseSummaries(), "Identifier", "Name");

				model.Form.Command = new CreateCollectionCommand();
				model.Form.Command.ReleaseDate = DateTime.Now;
				model.Form.ArtistIdentifier = artistId;
				model.Form.FormAction = "Create";

				return GetResult(View(model), View("CollectionWorkForm", model.Form));
			}
			catch (Exception ex)
			{
				return GetFailure(ex, this.ReturnToWorks(artistId));
			}
		}

		/// <summary>
		/// Processes the create collection work form post
		/// </summary>
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Create(string artistId, CreateCollectionCommand command)
		{
			try
			{
				command.ArtistIdentifier = artistId;
				this.ProcessRequest<CreateCollectionCommandResponse>(command);
				UserMessage message = new UserMessage(command.Title + " was created", UserMessageType.Info);
				return this.PostResult(message, this.ReturnToWorks(artistId));
			}
			catch (Exception ex)
			{
				var model = new CreateViewModel();
				model.Form = new CollectionWorkFormViewModel();
				model.Form.AvailableLicenses = new SelectList(this.GetLicenseSummaries(), "Identifier", "Name");
				model.Form.Command = new CreateCollectionCommand();
				model.Form.ArtistIdentifier = artistId;
				model.Form.FormAction = "Create";
				return this.PostFailure(ex, View(model));
			}
		}

		#endregion

		#region Edit

		/// <summary>
		/// display edit collection work form
		/// </summary>
		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Edit(string artistId, string workId)
		{
			try
			{
				var query = new GetCollectionDetailQuery();
				query.ArtistIdentifier = artistId;
				query.CollectionIdentifier = workId;
				var collection = this.ProcessRequest<GetCollectionDetailQueryResponse>(query).Collection;

				var model = new EditViewModel();
				model.Form = new CollectionWorkFormViewModel();
				model.Form.FormAction = "Edit";
				model.Form.ArtistIdentifier = artistId;
				model.Form.CollectionIdentifier = workId;

				model.Form.Command = new UpdateCollectionCommand();
				model.Form.Command.Description = collection.Description;
				model.Form.Command.LicenseIdentifier = collection.License.Identifier;
				model.Form.Command.ReleaseDate = collection.ReleaseDate;
				model.Form.Command.Tags = collection.Tags;
				model.Form.Command.Title = collection.Title;
				model.Form.AvailableLicenses = new SelectList(this.GetLicenseSummaries(), "Identifier", "Name", collection.License.Identifier); 
				
				return GetResult(View(model), View("CollectionWorkForm", model.Form));
			}
			catch (Exception ex)
			{
				return this.GetFailure(ex, this.ReturnToWorks(artistId));
			}
		}

		/// <summary>
		/// Processes the edit collection work form post
		/// </summary>
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Edit(string artistId, string workId, UpdateCollectionCommand command)
		{
			try
			{
				command.ArtistIdentifier = artistId;
				command.CollectionIdentifier = workId;
				var response = this.ProcessRequest<UpdateCollectionCommandResponse>(command);
				UserMessage message = new UserMessage(response.Title +  " was updated", UserMessageType.Info);
				return this.PostResult(message, this.ReturnToWorks(artistId));	
			}
			catch (Exception ex)
			{
				return this.PostFailure(ex, this.ReturnToWorks(artistId));
			}
		}

		/// <summary>
		/// display delete collection work form
		/// </summary>
		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Delete(string artistId, string workId)
		{
			try
			{
				var query = new GetCollectionDetailQuery();
				query.ArtistIdentifier = artistId;
				query.CollectionIdentifier = workId;

				var model = new DeleteViewModel();
				model.Collection = this.ProcessRequest<GetCollectionDetailQueryResponse>(query).Collection;

				return GetResult(View(model), View("CollectionWorkForm", model));
			}
			catch (Exception ex)
			{
				return this.GetFailure(ex, this.ReturnToWorks(artistId));
			}
		}

		/// <summary>
		/// Processes the delete collection work form post
		/// </summary>
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Delete(string artistId, string workId, FormCollection collection)
		{
			try
			{
				DeleteCollectionCommand command = new DeleteCollectionCommand();
				command.ArtistIdentifier = artistId;
				command.CollectionIdentifier = workId;
				var response = this.ProcessRequest<DeleteCollectionCommandResponse>(command);
				UserMessage message = new UserMessage(response.CollectionTitle + " was deleted", UserMessageType.Info);
				return this.PostResult(message, this.ReturnToWorks(artistId));
			}
			catch (Exception ex)
			{
				return this.PostFailure(ex, this.ReturnToWorks(artistId));
			}
		}

		#endregion

		#region Image

		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult RemoveImage(string artistId, string workId)
		{
			try
			{
				var command = new RemoveCollectionImageCommand();
				command.ArtistIdentifier = artistId;
				command.CollectionIdentifier = workId;
				this.ProcessRequest<RemoveCollectionImageCommandResponse>(command);

				if ( Request.IsAjaxRequest() )
				{
					var message = new
					{
						ImageUrl = this.Url.Action("Collection", "Image", new
						{
							id = Guid.Empty.ToString(),
							width = this.ImageWidth
						})
					};
					return Json(message);
				}
				else
				{
					return PostResult(new UserMessage("Image was removed", UserMessageType.Info), this.ReturnToWorks(artistId));
				}
			}
			catch (Exception ex)
			{
				return this.PostFailure(ex, this.ReturnToWorks(artistId));
			}
		}

		/// <summary>
		/// processes the upload image form post
		/// </summary>
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult UploadImage(string artistId, string workId, FormCollection formCollection)
		{
			try
			{

				var command = new UpdateCollectionImageCommand();
				command.ArtistIdentifier = artistId;
				command.CollectionIdentifier = workId;
				HttpPostedFileBase postedFile = Request.Files[0];
				command.FileName = postedFile.FileName;
				command.InputStream = postedFile.InputStream;
				var response = this.ProcessRequest<UpdateCollectionImageCommandResponse>(command);

				if ( formCollection.AllKeys.Contains("ajax") )
				{
					var message = new
					{
						ImageUrl = this.Url.Action("Collection", "Image", new
						{
							id = response.NewImageId.ToString(),
							width = this.ImageWidth
						})
					};
					return Json(message);
				}
				else
				{
					return PostResult(new UserMessage("Image was updated", UserMessageType.Info), this.ReturnToWorks(artistId));
				}
				
			}
			catch (Exception ex)
			{
				return this.PostFailure(ex, this.ReturnToWorks(artistId));
			}
		}

		#endregion
	}
}