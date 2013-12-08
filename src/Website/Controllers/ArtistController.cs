using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MusicCompany.Common.Commands;
using MusicCompany.Common.Queries;
using MusicCompany.Website.Models;
using MusicCompany.Website.Models.Artist;

namespace MusicCompany.Website.Controllers
{
	public class ArtistController : ExtendedController
	{
		#region Public Properties

		public int PageSize
		{
			get;
			set;
		}

		public int ProfilePictureWidth
		{
			get;
			set;
		}

		#endregion

		#region Private Methods

		private ActionResult ReturnToYourMusic()
		{
			return RedirectToAction("Index", "YourMusic");
		}

		#endregion

		/// <summary>
		/// List all artists
		/// </summary>
		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Index(int? page)
		{
			try
			{
				var query = new ListArtistsQuery();
				query.Paging = new PagingSpecification(page ?? 1, this.PageSize);
				var response = this.ProcessRequest<ListArtistsResponse>(query);
				var model = new IndexViewModel();
				model.Artists = response.Artists;
				return this.GetResult(View(model), null);
			}
			catch (Exception ex)
			{
				return this.GetFailure(ex, this.GoHome());
			}
		}

		/// <summary>
		/// Display artist's profile
		/// </summary>
		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Profile(string artistId)
		{
			try
			{
				var query = new GetArtistDetailQuery();
				query.Identifier = artistId;
				var response = this.ProcessRequest<GetArtistDetailResponse>(query);
				var model = new ProfileViewModel();
				model.ProfilePictureWidth = this.ProfilePictureWidth;
				model.Artist = response.Artist;
				return GetResult(View(model), View("ProfileDetail", model));
			}
			catch (Exception ex)
			{
				return GetFailure(ex, User.Identity.IsAuthenticated ? this.ReturnToYourMusic() : this.GoHome());
			}
		}

		/// <summary>
		/// Display artist's bio
		/// </summary>
		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Bio(string artistId)
		{
			try
			{
				var query = new GetArtistDetailQuery();
				query.Identifier = artistId;
				var response = this.ProcessRequest<GetArtistDetailResponse>(query);
				var model = new ProfileViewModel();
				model.ProfilePictureWidth = this.ProfilePictureWidth;
				model.Artist = response.Artist;
				return GetResult(View(model), View("Bio", model));
			}
			catch (Exception ex)
			{
				return GetFailure(ex, User.Identity.IsAuthenticated ? this.ReturnToYourMusic() : this.GoHome());
			}
		}

		/// <summary>
		/// Display artist's bio form
		/// </summary>
		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult BioForm(string artistId)
		{
			//TODO: I question the neccessity of this - why not just have jQuery display and populate the form
			// instead of having to do a query to populate it?
			try
			{
				var query = new GetArtistDetailQuery();
				query.Identifier = artistId;
				var response = this.ProcessRequest<GetArtistDetailResponse>(query);
				var model = new BioFormViewModel();
				model.Command = new UpdateArtistProfileCommand();
				model.Command.Bio = response.Artist.Bio;
				model.Command.Identifier = artistId;
				model.ArtistName = response.Artist.Name;
				return GetResult(null, View("BioForm", model));
			}
			catch (Exception ex)
			{
				return GetFailure(ex, User.Identity.IsAuthenticated ? this.ReturnToYourMusic() : this.GoHome());
			}
		}

		[Authorize]
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult BioForm(string artistId, UpdateArtistProfileCommand command)
		{
			try
			{
				command.Identifier = artistId;
				var response = this.ProcessRequest<UpdateArtistProfileCommandResponse>(command);
				UserMessage message = new UserMessage("Bio was updated", UserMessageType.Info);
				return PostResult(message, this.ReturnToYourMusic());
			}
			catch (Exception ex)
			{
				var model = new BioFormViewModel();
				model.Command = command;
				return PostFailure(ex ,View("BioForm", model));
			}
		}

		/// <summary>
		/// Display create new artist form
		/// </summary>
		[Authorize]
		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Create()
		{
			try
			{
				var model = new CreateViewModel();
				model.Command = new CreateArtistCommand();
				return GetResult(View(model), null);
			}
			catch (Exception ex)
			{
				return this.GetFailure(ex, this.ReturnToYourMusic());
			}
		}

		/// <summary>
		/// Processes create new artist form post
		/// </summary>
		[Authorize]
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Create(CreateArtistCommand command)
		{
			try
			{
				command.OwningPersonUsername = User.Identity.Name;
				var response = this.ProcessRequest<CreateArtistCommandResponse>(command);
				UserMessage message = new UserMessage(response.ArtistName + " was created", UserMessageType.Info);
				return PostResult(message, this.ReturnToYourMusic());
			}
			catch (Exception ex)
			{
				var model = new CreateViewModel();
				model.Command = command;
				return this.PostFailure(ex, View(model));
			}
		}

		[Authorize]
		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult EditProfile(string artistId)
		{
			try
			{
				var query = new GetArtistDetailQuery();
				query.Identifier = artistId;
				var response = this.ProcessRequest<GetArtistDetailResponse>(query);
				var model = new ProfileViewModel();
				model.Artist = response.Artist;
				model.ProfilePictureWidth = this.ProfilePictureWidth;
				return GetResult(View(model), null);
			}
			catch (Exception ex)
			{
				return GetFailure(ex, this.ReturnToYourMusic());
			}
		}

		[Authorize]
		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Delete(string artistId)
		{
			try
			{
				var model = new DeleteViewModel();
				model.ArtistIdentifier = artistId;
				return GetResult(View(model), null);
			}
			catch (Exception ex)
			{
				return GetFailure(ex, this.ReturnToYourMusic());
			}
		}

		[Authorize]
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Delete(string artistId, FormCollection collection)
		{
			try
			{
				var command = new DeleteArtistCommand();
				command.Identifier = artistId;
				var response = this.ProcessRequest<DeleteArtistCommandResponse>(command);
				UserMessage message = new UserMessage(response.ArtistName + " was deleted", UserMessageType.Info);
				return PostResult(message, this.ReturnToYourMusic());
			}
			catch (Exception ex)
			{
				return PostFailure(ex, this.ReturnToYourMusic());
			}
		}

		/// <summary>
		/// processes the upload image form post
		/// </summary>
		[Authorize]
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult UploadProfileImage(string artistId, FormCollection formCollection)
		{
			try
			{
				HttpPostedFileBase postedFile = Request.Files[0];

				var command = new UpdateArtistProfilePictureCommand();
				command.ArtistIdentifier = artistId;
				command.FileName = postedFile.FileName;
				command.InputStream = postedFile.InputStream;
				var response = this.ProcessRequest<UpdateArtistProfilePictureCommandResponse>(command);

				if (formCollection.AllKeys.Contains("ajax"))
				{
					var message = new
					{
						ImageUrl = this.Url.Action("Artist", "Image", new
						{
							id = response.NewImageId,
							width=this.ProfilePictureWidth
						})
					};
					return Json(message);
				}
				else
				{
					return PostResult(new UserMessage("Profile picture was updated", UserMessageType.Info),  this.ReturnToYourMusic());
				}
			}
			catch (Exception ex)
			{
				return this.PostFailure(ex,  this.ReturnToYourMusic());
			}
		}

		
		public ActionResult IsNameAvailable(string artistName, string artistId)
		{
			ArtistNameAvailableQuery query = new ArtistNameAvailableQuery();
			query.PotentialName = artistName;
			query.ExcludeIdentifier = artistId;
			var response = this.ProcessRequest<ArtistNameAvailableResponse>(query);
			bool available = response.IsAvailable;
			JsonResult result = Json(available);
			return result;
		}
	}
}
