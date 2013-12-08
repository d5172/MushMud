using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MusicCompany.Core.Repositories;
using System.IO;

namespace MusicCompany.Core.Services
{
	public class WorkServiceBase
	{

		public IWorkRepository WorkRepository
		{
			get;
			set;
		}

		protected int GetNextViewOrder(Work collection)
		{
			var items = this.WorkRepository.ListByCollection(collection);
			if (items.Count > 0)
			{
				return items.Last().ViewOrder + 1;
			}
			else
			{
				return 1;
			}
		}

		protected byte[] GetFileBytes(Stream dataStream)
		{
			byte[] fileBytes = new byte[dataStream.Length];
			dataStream.Read(fileBytes, 0, (int)dataStream.Length);
			return fileBytes;
		}
	}
}
