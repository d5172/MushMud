using System;

namespace MusicCompany.Core
{
	public class HistoricWorkLicense : Entity
	{

		protected HistoricWorkLicense() : base()
		{
		}

		public HistoricWorkLicense(Work work, License license, DateTime startDate, DateTime endDate)
		{
			if (work == null)
			{
				throw new ArgumentNullException("work");
			}
			if (license == null)
			{
				throw new ArgumentNullException("license");
			}
			if (DateTime.Compare(startDate, endDate) > 0)
			{
				throw new DomainLogicException("StartDate must be before EndDate");
			}
			this.Work = work;
			this.License = license;
			this.StartDate = startDate;
			this.EndDate = endDate;
		}

		public virtual DateTime StartDate
		{
			get;
			protected set;
		}

		public virtual DateTime EndDate
		{
			get;
			protected set;
		}

		public virtual Work Work
		{
			get;
			protected set;
		}

		public virtual License License
		{
			get;
			protected set;
		}
	}
}
