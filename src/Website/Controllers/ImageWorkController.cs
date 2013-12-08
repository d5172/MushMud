using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using MusicCompany.Website.Models.ImageWork;
using MusicCompany.Common.Queries;

namespace MusicCompany.Website.Controllers
{
    public class ImageWorkController : ExtendedController
    {
		[AcceptVerbs(HttpVerbs.Get)]
        public ActionResult Collection(string artistId, string workId, int? page)
        {
			try
			{
				var request = new ListCollectionImagesQuery();
				request.ArtistIdentifier = artistId;
				request.CollectionIdentifier = workId;
				request.Paging.Size = 1;
				request.Paging.Number = page ?? 1;

				var response = this.ProcessRequest<ListCollectionImagesQueryResponse>(request);
				var model = new CollectionImageViewModel();
				model.Images = response.Images;

				return this.GetResult(View(model), View("CollectionImageList", model));
			}
			catch ( Exception ex )
			{
				return this.GetFailure(ex, this.GoHome());
			}
        }

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Artist(string artistId, int? page)
		{
			var request = new ListArtistImagesQuery();
			request.ArtistIdentifier = artistId;
			request.Paging.Size = 1;
			request.Paging.Number = page ?? 1;

			var response = this.ProcessRequest<ListArtistImagesQueryResponse>(request);
			var model = new ArtistImageViewModel();
			model.Images = response.Images;

			return this.GetResult(View(model), View("ArtistImageList", model));

		}
    }
}