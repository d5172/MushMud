
namespace MusicCompany.Core
{
	public class License : VersionedEntity
	{

		protected License() : base()
		{
		}

		public License(string abbreviation, string title, string url)
		{
			if (string.IsNullOrEmpty(abbreviation))
			{
				throw new DomainLogicException("Abbreviation cannot be blank");
			}
			if (string.IsNullOrEmpty(title))
			{
				throw new DomainLogicException("Title cannot be blank");
			}
			if (string.IsNullOrEmpty(url))
			{
				throw new DomainLogicException("URL cannot be blank");
			}
			this.Abbreviation = abbreviation;
			this.Title = title;
			this.Url = url;
			this.Description = "";
			this.ImageUrl = "";
		}

		public virtual string Url
		{
			get;
			protected set;
		}

		public virtual string ImageUrl
		{
			get;
			set;
		}

		public virtual int ViewOrder
		{
			get;
			protected set;
		}

		public virtual string Abbreviation
		{
			get;
			protected set;
		}

		public virtual string Title
		{
			get;
			protected set;
		}

		public virtual string FullName
		{
			get
			{
				return string.Format("Creative Commons {0} 3.0", this.Title);
			}
		}

		public virtual string Description
		{
			get;
			set;
		}
	}
}