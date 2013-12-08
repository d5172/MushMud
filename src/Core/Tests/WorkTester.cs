using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using MusicCompany.Core.Repositories;
using MusicCompany.Core.Services;

namespace MusicCompany.Core.Tests
{
	[TestFixture]
	public class WorkTester
	{
		private Artist artist;
		private Person person;
		
		private AudioWork CreateAudioWork(string title)
		{
			var data = new BinaryFileData(new byte[1]{1});
			var info = new AudioFileInfo("Test", FileFormat.MP3, 0, data, 0);
			return new AudioWork(artist, title, info, new License("test", "test", "test"), DateTime.Now);
		}

		[SetUp]
		public void Setup()
		{
			this.person = new Person("Test", "Test Person");
			this.artist = new Artist("Test Artist", person);
		}

		[Test]
		public void CanAddNewTags()
		{
			//arrange
			Work subject = this.CreateAudioWork("CanSetTags");
			string[] tags = "One,Two,Three".Split(',');
			ITagRepository tagRepository = new TagRepository();
			ITagService tagService = new TagService()
			{
				TagRepository = tagRepository
			};
			
			//act
			subject.SetTags(tags, tagService);

			//assert
			Assert.AreEqual(3, subject.ListTags().Count(), "ListTags() not right count");
			foreach (string tag in tags)
			{
				Assert.IsNotNull(subject.ListTags().SingleOrDefault(t => t.Lemma == tag), "ListTags did not contain " + tag);

				Assert.IsNotNull(tagRepository.GetByLemma(tag), tag + " not added to repository");
			}
		}

		[Test]
		public void CanAddUntrimmedTags()
		{
			//arrange
			Work subject = this.CreateAudioWork("CanSetTags");
			string[] tags = "One, Two,  Three,Four".Split(',');
			ITagRepository tagRepository = new TagRepository();
			ITagService tagService = new TagService()
			{
				TagRepository = tagRepository
			};

			//act
			subject.SetTags(tags, tagService);

			//assert
			Assert.AreEqual(4, subject.ListTags().Count(), "ListTags() not right count");
			foreach (string tag in tags.Select(t => t.Trim()))
			{
				Assert.IsNotNull(subject.ListTags().SingleOrDefault(t => t.Lemma == tag), "ListTags did not contain " + tag);

				Assert.IsNotNull(tagRepository.GetByLemma(tag), tag + " not added to repository");
			}
		}


		[Test]
		public void CanAddExistingTags()
		{
			//arrange
			Work subject = this.CreateAudioWork("CanSetTags");
			string[] tags = "One,Two,Three".Split(',');
			ITagRepository tagRepository = new TagRepository();
			ITagService tagService = new TagService()
			{
				TagRepository = tagRepository
			};
			foreach (string tag in tags)
			{
				tagRepository.Save(new Tag(tag));
			}
			
			//act
			subject.SetTags(tags, tagService);

			//assert
			Assert.AreEqual(3, subject.ListTags().Count(), "ListTags() not right count");
			foreach (string tag in tags)
			{
				Assert.IsNotNull(subject.ListTags().SingleOrDefault(t => t.Lemma == tag), "ListTags did not contain " + tag);
			}
		}

		[Test]
		public void CanUpdateTagsWithoutChange()
		{
			//arrange
			Work subject = this.CreateAudioWork("CanSetTags");
			string[] tags = "One,Two,Three".Split(',');
			ITagRepository tagRepository = new TagRepository();
			ITagService tagService = new TagService()
			{
				TagRepository = tagRepository
			};

			//act
			subject.SetTags(tags, tagService);
			subject.SetTags(tags, tagService);

			//assert
			Assert.AreEqual(3, subject.ListTags().Count(), "ListTags() not right count");
			foreach (string tag in tags)
			{
				Assert.IsNotNull(subject.ListTags().SingleOrDefault(t => t.Lemma == tag), "ListTags did not contain " + tag);
			}
		}

		[Test]
		public void CanRemoveAllTags()
		{
			//arrange
			Work subject = this.CreateAudioWork("CanSetTags");
			string[] tags = "One,Two,Three".Split(',');
			ITagRepository tagRepository = new TagRepository();
			ITagService tagService = new TagService()
			{
				TagRepository = tagRepository
			};

			//act
			subject.SetTags(tags, tagService);
			subject.SetTags(new string[] { }, tagService);

			//assert
			Assert.AreEqual(0, subject.ListTags().Count(), "ListTags() not right count");
		}

		[Test]
		public void CanReplaceTags()
		{
			//arrange
			Work subject = this.CreateAudioWork("CanSetTags");
			string[] tags = "One,Two,Three".Split(',');
			ITagRepository tagRepository = new TagRepository();
			ITagService tagService = new TagService()
			{
				TagRepository = tagRepository
			};

			//act
			subject.SetTags(tags, tagService);
			tags = "A B C".Split(' ');
			subject.SetTags(tags, tagService);

			//assert
			Assert.AreEqual(3, subject.ListTags().Count(), "ListTags() not right count");
			foreach (string tag in tags)
			{
				Assert.IsNotNull(subject.ListTags().SingleOrDefault(t => t.Lemma == tag), "ListTags did not contain " + tag);
			}
		}

		[Test]
		public void CanAppendTags()
		{
			//arrange
			Work subject = this.CreateAudioWork("CanSetTags");
			string[] tags = "One,Two,Three".Split(',');
			ITagRepository tagRepository = new TagRepository();
			ITagService tagService = new TagService()
			{
				TagRepository = tagRepository
			};

			//act
			subject.SetTags(tags, tagService);

			List<string> list = new List<string>(tags);
			list.Add("Four");
			tags = list.ToArray();

			subject.SetTags(tags, tagService);

			//assert
			Assert.AreEqual(4, subject.ListTags().Count(), "ListTags() not right count");
			foreach (string tag in tags)
			{
				Assert.IsNotNull(subject.ListTags().SingleOrDefault(t => t.Lemma == tag), "ListTags did not contain " + tag);
			}
		}

		[Test]
		public void CanRemoveTag()
		{
			//arrange
			Work subject = this.CreateAudioWork("CanSetTags");
			string[] tags = "One,Two,Three".Split(',');
			ITagRepository tagRepository = new TagRepository();
			ITagService tagService = new TagService()
			{
				TagRepository = tagRepository
			};

			//act
			subject.SetTags(tags, tagService);

			List<string> list = new List<string>(tags);
			list.Remove("Three");
			tags = list.ToArray();

			subject.SetTags(tags, tagService);

			//assert
			Assert.AreEqual(2, subject.ListTags().Count(), "ListTags() not right count");
			foreach (string tag in tags)
			{
				Assert.IsNotNull(subject.ListTags().SingleOrDefault(t => t.Lemma == tag), "ListTags did not contain " + tag);
			}
		}

		[Test]
		public void CanAddAndRemoveTag()
		{
			//arrange
			Work subject = this.CreateAudioWork("CanSetTags");
			string[] tags = "One,Two,Three".Split(',');
			ITagRepository tagRepository = new TagRepository();
			ITagService tagService = new TagService()
			{
				TagRepository = tagRepository
			};

			//act
			subject.SetTags(tags, tagService);

			List<string> list = new List<string>(tags);
			list.Remove("Three");
			list.Add("A");
			tags = list.ToArray();

			subject.SetTags(tags, tagService);

			//assert
			Assert.AreEqual(3, subject.ListTags().Count(), "ListTags() not right count");
			foreach (string tag in tags)
			{
				Assert.IsNotNull(subject.ListTags().SingleOrDefault(t => t.Lemma == tag), "ListTags did not contain " + tag);
			}
		}

		[Test]
		public void CanTryToAddTheSameTags()
		{
			//arrange
			Work subject = this.CreateAudioWork("CanSetTags");
			string[] tags = "One,Two,Three".Split(',');
			ITagRepository tagRepository = new TagRepository();
			ITagService tagService = new TagService()
			{
				TagRepository = tagRepository
			};

			//act
			subject.SetTags(tags, tagService);


			subject.SetTags(tags, tagService);

			//assert
			Assert.AreEqual(3, subject.ListTags().Count(), "ListTags() not right count");
			foreach (string tag in tags)
			{
				Assert.IsNotNull(subject.ListTags().SingleOrDefault(t => t.Lemma == tag), "ListTags did not contain " + tag);
			}
		}

		class TagRepository : ITagRepository
		{
			private IDictionary<Guid, Tag> dictionary = new Dictionary<Guid, Tag>();

			public IList<Tag> ListStartingWith(string startingWith)
			{
				throw new NotImplementedException();
			}

			public Tag GetByLemma(string lemma)
			{
				return this.dictionary.Values.Where(t => t.Lemma.StartsWith(lemma)).SingleOrDefault();
			}

			public IOrderedQueryable<Tag> AsQueryable()
			{
				throw new NotImplementedException();
			}

			public Tag Get(Guid id)
			{
				return this.dictionary[id];
			}

			public void Save(Tag instance)
			{
				this.dictionary.Add(instance.Id, instance);
			}

			public void Delete(Tag instance)
			{
				throw new NotImplementedException();
			}
		}
	}
}
