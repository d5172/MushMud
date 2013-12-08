using PagedList;

namespace MusicCompany.Website.Extensions
{
	public static class PagedListExtensions
	{
		public static int StartItem<T>(this IPagedList<T> pagedList)
		{
			return (pagedList.PageSize * pagedList.PageNumber) - (pagedList.PageSize - 1);
		}

		public static int EndItem<T>(this IPagedList<T> pagedList)
		{
			return pagedList.StartItem<T>() + (pagedList.Count - 1);
		}
	}
}
