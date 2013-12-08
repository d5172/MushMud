using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using MusicCompany.Core;
using System.IO;

namespace MusicCompany.Core.Tests
{
	[TestFixture]
	public class UniqueNameTester
	{

		[Test]
		public void CanPreserveValue()
		{
			string input = "test";
			UniqueName subject = new UniqueName(input);
			Assert.AreEqual(input, subject.Value);
		}

		[Test]
		public void RemovesSpaces()
		{
			string input = "This is a test";
			UniqueName subject = new UniqueName(input);
			Assert.IsFalse(subject.Id.Contains(" "));
		}

		[Test]
		public void CanPreserveValue_InvalidPathChars()
		{
			string input = @"~`!@#$%^&*()_+-={}|[]:";
			UniqueName subject = new UniqueName(input);
			Assert.AreEqual(input, subject.Value);
		}
	}
}
