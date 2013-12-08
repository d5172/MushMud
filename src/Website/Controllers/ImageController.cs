using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;
using MusicCompany.Common.Queries;
using MusicCompany.Common.ViewModel;
using MusicCompany.Core;
using MusicCompany.Core.Services;
using IO = System.IO;

namespace MusicCompany.Website.Controllers
{
	public class ImageController : ExtendedController
	{
		#region Private Fields

		private static readonly int defaultResolution = 72;

		// we can use 1 year since these dynamically generated images are never modified.
		private static readonly int cacheExpirationMinutes = 525600; 

		#endregion

		#region Properties

		public IImageProcessingService ImageProcessingService
		{
			get;
			set;
		}

		public string ImageFileCachePath
		{
			get;
			set;
		}

		public string DefaultCollectionImage
		{
			get;
			set;
		}

		public string DefaultArtistImage
		{
			get;
			set;
		}

		public string DefaultPersonImage
		{
			get;
			set;
		}

		public int MaxAllowedWidth
		{
			get;
			set;
		}

		public int DefaultImageWidth
		{
			get;
			set;
		}

		#endregion

		#region Action Methods

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Index(Guid id, int width)
		{
			this.SetCaching();
			if ( id == Guid.Empty )
			{
				return new EmptyResult();
			}
			else
			{
				return this.RenderResizedImage(id, width);
			}
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Collection(Guid id, int width)
		{
			this.SetCaching();
			if ( id == Guid.Empty )
			{
				return this.File(this.DefaultCollectionImage, FileFormat.Png.ToMimeType());
			}
			else
			{
				return this.RenderResizedImage(id, width);
			}
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Artist(Guid id, int width)
		{
			this.SetCaching();
			if ( id == Guid.Empty )
			{
				return this.File(this.DefaultArtistImage, FileFormat.Png.ToMimeType());
			}
			else
			{
				return this.RenderResizedImage(id, width);
			}
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Person(Guid id, int width)
		{
			this.SetCaching();
			if ( id == Guid.Empty )
			{
				return this.File(this.DefaultPersonImage, FileFormat.Png.ToMimeType());
			}
			else
			{
				return this.RenderResizedImage(id, width);
			}
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult FullSize(Guid id, string title)
		{
			if ( id == Guid.Empty )
			{
				return new EmptyResult();
			}
			return this.RenderFullSizeImage(id, title);
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// This renders the image resized to the specified width, as a JPeg.
		/// (This employs file-system caching for performance.)
		/// </summary>
		private ActionResult RenderResizedImage(Guid id, int width)
		{
			if ( width  > MaxAllowedWidth )
			{
				width = this.DefaultImageWidth;
			}
			string fileName = string.Format("{0}_{1}", id, width);
			string directory = Server.MapPath(this.ImageFileCachePath);
			string path = IO.Path.Combine(directory, fileName);
			if ( !IO.File.Exists(path) )
			{
				if ( !IO.Directory.Exists(directory) )
				{
					IO.Directory.CreateDirectory(directory);
				}
				GetImageDetailQuery query = new GetImageDetailQuery();
				query.Id = id;
				GetImageDetailResponse response = this.ProcessRequest<GetImageDetailResponse>(query);
				ImageDetailView imageFile = response.Image;
				using ( var stream = new IO.MemoryStream(imageFile.Data) )
				{
					using ( Bitmap bitmap = this.ImageProcessingService.Resize(stream, width, defaultResolution, defaultResolution) )
					{
						bitmap.Save(path, ImageFormat.Jpeg);
					}
				}
			}
			return this.File(path, FileFormat.Jpeg.ToMimeType());
		}

		private ActionResult RenderFullSizeImage(Guid id, string title)
		{
			GetImageDetailQuery query = new GetImageDetailQuery();
			query.Id = id;
			GetImageDetailResponse response = this.ProcessRequest<GetImageDetailResponse>(query);
			ImageDetailView imageFile = response.Image;
			string fileDownloadName;
			if ( !string.IsNullOrEmpty(title) )
			{
				string extension = Path.GetExtension(imageFile.OriginalFileName);
				if ( !string.IsNullOrEmpty(extension) )
				{
					fileDownloadName = title.Trim() + Path.GetExtension(imageFile.OriginalFileName);
				}
				else
				{
					fileDownloadName = title.Trim();
				}
			}
			else
			{
				fileDownloadName = imageFile.OriginalFileName;
			}
			return this.File(imageFile.Data, imageFile.FileFormat, fileDownloadName);
		}

		private void SetCaching()
		{
			Response.CacheControl = "Public";
			Response.Expires = cacheExpirationMinutes;
		}

		#endregion
	}
}