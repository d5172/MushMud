using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace MusicCompany.Core.Tests
{

	[TestFixture]
	public class ArtistTester
	{

		private Artist artist;
		private Person person;

		private AudioWork CreateAudioWork(string title)
		{
			var data = new BinaryFileData(new byte[1]{1});
			var info = new AudioFileInfo("Test", FileFormat.MP3, 0, data, 0);
			return new AudioWork(artist, title, info, new License("test","test","test"), DateTime.Now);
		}

		[SetUp]
		public void SetupArtistAndPerson()
		{
			this.person = new Person("Test", "Test Person");
			this.artist = new Artist("Test Artist", person);
		}
	
		
		[Test]
		public void CanCreateArtist()
		{
			Assert.AreEqual("Test Artist", artist.Name.Value, "Artist name not set");
			Assert.IsNotNull(artist.EnumerateArtistPersons(), "EnumerateArtistPersons returned null");
			Assert.IsNotNull(artist.GetCollectionWorks(), "EnumerateCollectionWorks returned null");
			Assert.IsNotNull(artist.GetSingleWorks(), "EnumerateSingleWorks returned null");
		}

		[Test]
		public void ArtistCanBeManagedByOwner()
		{
			Assert.IsTrue(artist.IsManagedBy(person), "Artist not managed by owningPerson");
		}

		[Test]
		public void CanAddSingleWork()
		{
			var work = this.CreateAudioWork("test");
			this.artist.AddSingleWork(work);
			Assert.AreEqual(1, this.artist.GetSingleWorks().Count(), "EnumerateSingleWorks did not return 1");
			Assert.AreSame(work, this.artist.GetSingleWorks().First(), "EnumerateSingleWorks First was not new Work");
			Assert.AreEqual(0, work.ViewOrder, "Work ViewOrder was not 0");
		}

		[Test]
		public void CanRemoveSingleWork()
		{
			var work = this.CreateAudioWork("test");
			this.artist.AddSingleWork(work);
			this.artist.RemoveSingleWork(work);
			Assert.AreEqual(0, this.artist.GetSingleWorks().Count(), "EnumerateSingleWorks did not return 0");
		}

		[Test]
		public void CanAddTwoWorks()
		{
			var work1 = this.CreateAudioWork("A");
			this.artist.AddSingleWork(work1);
			var work2 = this.CreateAudioWork("B");
			this.artist.AddSingleWork(work2);

			Assert.AreEqual(2, this.artist.GetSingleWorks().Count(), "EnumerateSingleWorks did not return 2");
			Assert.AreSame(work1, this.artist.GetSingleWorks().First(), "EnumerateSingleWorks First was not work1");
			Assert.AreEqual(0, work1.ViewOrder, "Work1 ViewOrder was not 0");
			Assert.AreEqual(1, work2.ViewOrder, "Work2 ViewOrder was not 1");
		}

		[Test]
		public void CanAddTwoWorksAndRemoveSecond()
		{
			var work1 = this.CreateAudioWork("A");
			this.artist.AddSingleWork(work1);
			var work2 = this.CreateAudioWork("B");
			this.artist.AddSingleWork(work2);

			this.artist.RemoveSingleWork(work2);

			Assert.AreEqual(1, this.artist.GetSingleWorks().Count(), "EnumerateSingleWorks did not return 1");
			Assert.AreSame(work1, this.artist.GetSingleWorks().First(), "EnumerateSingleWorks First was not work1");
			Assert.AreEqual(0, work1.ViewOrder, "Work1 ViewOrder was not 0");
		}

		[Test]
		public void CanAddTwoWorksAndRemoveFirst()
		{
			var work1 = this.CreateAudioWork("A");
			this.artist.AddSingleWork(work1);
			var work2 = this.CreateAudioWork("B");
			this.artist.AddSingleWork(work2);

			this.artist.RemoveSingleWork(work1);

			Assert.AreEqual(1, this.artist.GetSingleWorks().Count(), "EnumerateSingleWorks did not return 1");
			Assert.AreSame(work2, this.artist.GetSingleWorks().First(), "EnumerateSingleWorks First was not work2");
			Assert.AreEqual(0, work2.ViewOrder, "Work2 ViewOrder was not 0");
		}

		[Test]
		public void CanAddTwoWorksWithSameName()
		{
			var work1 = this.CreateAudioWork("A");
			this.artist.AddSingleWork(work1);
			var work2 = this.CreateAudioWork("A");
			this.artist.AddSingleWork(work2);

			Assert.AreEqual(2, this.artist.GetSingleWorks().Count(), "EnumerateSingleWorks did not return 2");
			Assert.AreSame(work1, this.artist.GetSingleWorks().First(), "EnumerateSingleWorks First was not work1");
			Assert.AreEqual(0, work1.ViewOrder, "Work1 ViewOrder was not 0");
			Assert.AreEqual(1, work2.ViewOrder, "Work2 ViewOrder was not 1");
			Assert.AreNotEqual(work1.Title.Value, work2.Title.Value, "Work1 and Work2 have same title");

			Assert.AreEqual("A 1", work2.Title.Value, "Work2 did not get renamed");
		}

		[Test]
		public void CanAddThreeWorksWithSameName()
		{
			var work1 = this.CreateAudioWork("A");
			this.artist.AddSingleWork(work1);
			var work2 = this.CreateAudioWork("A");
			this.artist.AddSingleWork(work2);
			var work3 = this.CreateAudioWork("A");
			this.artist.AddSingleWork(work3);

			Assert.AreEqual("A 1", work2.Title.Value, "Work2 did not get renamed");
			Assert.AreEqual("A 2", work3.Title.Value, "Work3 did not get renamed");
		}
	}
}
