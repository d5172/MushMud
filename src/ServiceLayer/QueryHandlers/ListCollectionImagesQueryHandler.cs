using System.Linq;
using Agatha.Common;
using MusicCompany.Common.Queries;
using MusicCompany.Common.ViewModel;
using MusicCompany.ServiceLayer.Base;
using NHibernate.Linq;
using PagedList;

namespace MusicCompany.ServiceLayer.QueryHandlers
{
	public class ListCollectionImagesQueryHandler : QueryHandler<ListCollectionImagesQuery, ListCollectionImagesQueryResponse>
	{
		public override Response Handle(ListCollectionImagesQuery request)
		{
			var collection = this.Session.Linq<CollectionSummaryView>().Single(c => c.ArtistIdentifier == request.ArtistIdentifier && c.Identifier == request.CollectionIdentifier);
			var image = new ImageSummaryView();
			image.BinaryFileId = collection.BinaryFileId;
			image.Title = collection.Title;
			ImageSummaryView[] images = new ImageSummaryView[1];
			images[0] = image;
			var response = this.CreateTypedResponse();
			response.Images = new StaticPagedList<ImageSummaryView>(images, 0, 1, 1);
			response.ArtistName = collection.ArtistName;
			response.CollectionTitle = collection.Title;
			return response;
		}
	}
}