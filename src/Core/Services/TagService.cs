using System.Collections.Generic;
using System.Linq;
using NHibernate;
using NHibernate.Linq;

namespace MusicCompany.Core.Services
{
	public interface ITagService
	{
		void SetTags(IList<Tag> targetTags, string[] newLemmas);
	}

	public class TagService : ITagService
	{
		public ISessionFactory SessionFactory
		{
			get;
			set;
		}

		public void SetTags(IList<Tag> targetTags, string[] newLemmas)
		{
			ISession session = this.SessionFactory.GetCurrentSession();

			newLemmas = newLemmas.Select(l => l.Trim()).Where(l => !string.IsNullOrEmpty(l)).ToArray();

			//remove old where not in new
			foreach ( Tag existing in new List<Tag>(targetTags) )
			{
				if ( !newLemmas.Contains(existing.Lemma) )
				{
					targetTags.Remove(existing);
				}
			}

			//add new where not in old
			foreach ( string tagLemma in newLemmas )
			{
				if ( !targetTags.Select(t => t.Lemma).Contains(tagLemma) )
				{
					Tag newTag = session.Linq<Tag>().SingleOrDefault(t => t.Lemma == tagLemma);
					if ( newTag == null )
					{
						newTag = new Tag(tagLemma);
						session.Save(newTag);
					}
					targetTags.Add(newTag);
				}
			}
		}
	}
}
