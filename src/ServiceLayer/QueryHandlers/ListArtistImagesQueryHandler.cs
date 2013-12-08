using System.Linq;
using Agatha.Common;
using MusicCompany.Common.Queries;
using MusicCompany.Common.ViewModel;
using MusicCompany.ServiceLayer.Base;
using NHibernate.Linq;
using PagedList;

namespace MusicCompany.ServiceLayer.QueryHandlers
{
	public class ListArtistImagesQueryHandler : QueryHandler<ListArtistImagesQuery, ListArtistImagesQueryResponse>
	{
		public override Response Handle(ListArtistImagesQuery request)
		{
			var artist = this.Session.Linq<ArtistSummaryView>().Single(a => a.Identifier == request.ArtistIdentifier);
			var image = new ImageSummaryView();
			image.BinaryFileId = artist.ProfileImageId;
			image.Title = artist.Name;
			ImageSummaryView[] images = new ImageSummaryView[1];
			images[0] = image;
			var response = this.CreateTypedResponse();
			response.Images = new StaticPagedList<ImageSummaryView>(images, 0, 1, 1);
			response.ArtistName = artist.Name;
			return response;
		}
	}
}