using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MusicCompany.Common.Queries;

namespace MusicCompany.ServiceLayer.QueryHandlers
{
	public static class PagingSpecificationExtensions
	{
		public static int ItemIndex(this PagingSpecification paging)
		{
			return paging.Number - 1;
		}

		public static int GetSkipIndex(this PagingSpecification paging)
		{
			return (paging.ItemIndex()) * paging.Size;
		}
	}
}
