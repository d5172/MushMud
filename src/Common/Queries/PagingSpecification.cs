
namespace MusicCompany.Common.Queries
{
	public class PagingSpecification
	{
		public PagingSpecification()
		{
			this.Number = 1;
			this.Size = 50;
		}

		public PagingSpecification(int pageNumber, int pageSize)
		{
			this.Number = pageNumber;
			this.Size = pageSize;
		}

		public int Number
		{
			get;
			set;
		}

		public int Size
		{
			get;
			set;
		}
	}
}
