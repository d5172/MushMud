using System;
using System.Web.Mvc;
using MusicCompany.Common.Queries;
using MusicCompany.Website.Models.Music;

namespace MusicCompany.Website.Controllers
{
	public class MusicController : ExtendedController
	{
		#region Public Properties

		public int PageSize
		{
			get;
			set;
		}

		#endregion

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Index(int? page)
		{
			try
			{
				var query = new ListTopLevelWorksQuery();
				query.Paging.Number = page ?? 1;
				query.Paging.Size = this.PageSize;

				var response = this.ProcessRequest<ListTopLevelWorksResponse>(query);
				var model = new IndexViewModel();
				model.Works = response.Works;

				return this.GetResult(View(model), null);
			}
			catch ( Exception ex )
			{
				return this.GetFailure(ex, this.GoHome());
			}
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult ListTracks(string artistId, string workId)
		{
			try
			{
				var query = new ListTracksQuery();
				query.ArtistIdentifier = artistId;
				query.CollectionIdentifier = workId;
				var response = this.ProcessRequest<ListTracksQueryResponse>(query);
				var model = new ListTracksViewModel();
				model.Tracks = response.Tracks;

				return GetResult(new RedirectResult(Url.Action("Collection", new {artistId = artistId, workId = workId})), View("TrackList", model));
			}
			catch ( Exception ex )
			{
				return GetFailure(ex, null);
			}
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Collection(string artistId, string workId)
		{
			try
			{
				var query = new GetCollectionDetailQuery();
				query.ArtistIdentifier = artistId;
				query.CollectionIdentifier = workId;
				var response = this.ProcessRequest<GetCollectionDetailQueryResponse>(query);
				var model = new CollectionViewModel();
				model.Collection = response.Collection;
				if ( Request.IsAjaxRequest() )
				{
					return View("CollectionDetail", model.Collection);
				}
				else
				{
					return View(model);
				}
			}
			catch ( Exception ex )
			{
				return GetFailure(ex, null);
			}
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Track(string artistId, string workId, string collectionId)
		{
			try
			{
				var query = new GetTrackDetailQuery();
				query.ArtistIdentifier = artistId;
				query.WorkIdentifier = workId;
				query.CollectionIdentifier = collectionId;
				var response = this.ProcessRequest<GetTrackDetailQueryResponse>(query);
				var model = new TrackViewModel();
				model.Track = response.Track;
				if ( Request.IsAjaxRequest() )
				{
					return View("TrackDetail", model.Track);
				}
				else
				{
					return View(model);
				}
			}
			catch ( Exception ex )
			{
				return GetFailure(ex, null);
			}
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Single(string artistId, string workId)
		{
			try
			{
				var query = new GetAudioSingleDetailQuery();
				query.ArtistIdentifier = artistId;
				query.WorkIdentifier = workId;
				var response = this.ProcessRequest<GetAudioSingleDetailQueryResponse>(query);
				var model = new SingleViewModel();
				model.Single = response.Single;

				if ( Request.IsAjaxRequest() )
				{
					return View("SingleDetail", model.Single);
				}
				else
				{
					return View(model);
				}
			}
			catch ( Exception ex )
			{
				return GetFailure(ex, null);
			}
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult DownloadTrack(string artistId, string workId, string collectionId)
		{
			var trackQuery = new GetTrackDetailQuery();
			trackQuery.ArtistIdentifier = artistId;
			trackQuery.CollectionIdentifier = collectionId;
			trackQuery.WorkIdentifier = workId;
			var trackResponse = this.ProcessRequest<GetTrackDetailQueryResponse>(trackQuery);
			var track = trackResponse.Track;

			var collectionQuery = new GetCollectionDetailQuery();
			collectionQuery.ArtistIdentifier = artistId;
			collectionQuery.CollectionIdentifier = collectionId;
			var collectionResponse = this.ProcessRequest<GetCollectionDetailQueryResponse>(collectionQuery);
			var collection = collectionResponse.Collection;

			var model = new DownloadFormViewModel();
			model.ArtistName = collection.ArtistName;
			model.ArtistIdentifier = collection.ArtistIdentifier;
			model.License = collection.License;
			model.WorkTitle = track.Title;
			model.FileFormat = track.FileFormat;
			model.FormAction = Url.Action("DownloadTrack", "Stream", new
			{
				artistId = artistId,
				workId = workId,
				collectionId = collectionId,
				format = track.FileFormat
			});
			if ( !string.IsNullOrEmpty(track.AlternateFileFormat) )
			{
				model.AlternateFormAction = Url.Action("DownloadTrack", "Stream", new
				{
					artistId = artistId,
					workId = workId,
					collectionId = collectionId,
					format = track.AlternateFileFormat
				});
				model.AlternateFileFormat = track.AlternateFileFormat;
			}

			return GetResult(View(model), View("DownloadForm", model));
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult DownloadSingle(string artistId, string workId)
		{
			var query = new GetAudioSingleDetailQuery();
			query.ArtistIdentifier = artistId;
			query.WorkIdentifier = workId;
			var response = this.ProcessRequest<GetAudioSingleDetailQueryResponse>(query);
			var single = response.Single;

			var model = new DownloadFormViewModel();
			model.ArtistName = single.ArtistName;
			model.ArtistIdentifier = single.ArtistIdentifier;
			model.License = single.License;
			model.WorkTitle = single.Title;
			model.FileFormat = single.FileFormat;
			model.FormAction = Url.Action("DownloadSingle", "Stream", new
			{
				artistId = artistId,
				workId = workId,
				format = single.FileFormat
			});
			if ( !string.IsNullOrEmpty(single.AlternateFileFormat) )
			{
				model.AlternateFormAction = Url.Action("DownloadSingle", "Stream", new
				{
					artistId = artistId,
					workId = workId,
					format = single.AlternateFileFormat
				});
				model.AlternateFileFormat = single.AlternateFileFormat;
			}

			return GetResult(View(model), View("DownloadForm", model));
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Torrent(string artistId, string workId)
		{
			var collectionQuery = new GetCollectionDetailQuery();
			collectionQuery.ArtistIdentifier = artistId;
			collectionQuery.CollectionIdentifier = workId;
			var collectionResponse = this.ProcessRequest<GetCollectionDetailQueryResponse>(collectionQuery);
			var collection = collectionResponse.Collection;

			DownloadFormViewModel form = new DownloadFormViewModel();
			form.ArtistIdentifier = collection.ArtistIdentifier;
			form.ArtistName = collection.ArtistName;
			form.License = collection.License;
			form.WorkTitle = collection.Title;
			form.FormAction = Url.Action("Torrent", "Stream", new
			{
				artistId = artistId,
				workId = workId
			});
			return GetResult(View(form), View("TorrentForm", form));
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult DownloadCollection(string artistId, string workId)
		{
			var collectionQuery = new GetCollectionDetailQuery();
			collectionQuery.ArtistIdentifier = artistId;
			collectionQuery.CollectionIdentifier = workId;
			var collectionResponse = this.ProcessRequest<GetCollectionDetailQueryResponse>(collectionQuery);
			var collection = collectionResponse.Collection;

			DownloadFormViewModel form = new DownloadFormViewModel();
			form.ArtistIdentifier = collection.ArtistIdentifier;
			form.ArtistName = collection.ArtistName;
			form.License = collection.License;
			form.WorkTitle = collection.Title;
			form.FileFormat = "Zip";
			form.FormAction = Url.Action("Zip", "Stream", new
			{
				artistId = artistId,
				workId = workId
			});
			//TODO: uncomment when torrents are fully supported.
			//form.AlternateFileFormat = "Torrent";
			//form.AlternateFormAction = Url.Action("Torrent", "Stream", new
			//{
			//    artistId = artistId,
			//    workId = workId
			//});
			return GetResult(View(form), View("DownloadForm", form));
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Search(string terms, int? page)
		{
			try
			{
				if ( string.IsNullOrEmpty(terms) )
				{
					return RedirectToAction("Index");
				}
				Session["searchTerms"] = Server.UrlDecode(terms);
				string[] searchTerms = terms.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);

				var query = new WorkSearchQuery();
				query.SearchTerms = searchTerms;
				query.Paging.Number = page ?? 1;
				query.Paging.Size = this.PageSize;

				var response = this.ProcessRequest<WorkSearchQueryResponse>(query);

				var model = new SearchViewModel();
				model.List = response.Works;
				model.Terms = terms;

				return GetResult(View(model), null);
			}
			catch ( Exception ex )
			{
				return GetFailure(ex, this.GoHome());
			}
		}


		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult AutoCompleteSearchTerm(string q)
		{
			var query = new SuggestSearchTermsQuery();
			query.StartingWith = q;
			var response = this.ProcessRequest<SuggestSearchTermsQueryResponse>(query);
			string[] terms = response.SuggestedTerms;

			string plainTextContentResult;
			if ( terms.Length > 0 )
			{
				plainTextContentResult = string.Join(Environment.NewLine, terms);
			}
			else
			{
				plainTextContentResult = " ";	//Firefox complains about no element found if empty string
			}
			return Content(plainTextContentResult, "text/plain");
		}
	}
}
