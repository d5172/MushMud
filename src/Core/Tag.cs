
namespace MusicCompany.Core
{
	public class Tag : Entity
	{
		protected Tag()
			: base()
		{
		}

		public Tag(string lemma)
		{
			string trimmedLemma = lemma.Trim();
			if (string.IsNullOrEmpty(trimmedLemma))
			{
				throw new DomainLogicException("Cannot create an empty tag");
			}
			this.Lemma = trimmedLemma;
		}

		public virtual string Lemma
		{
			get;
			protected set;
		}
	}
}
