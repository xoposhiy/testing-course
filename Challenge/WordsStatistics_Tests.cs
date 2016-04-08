using System;
using System.Collections.Generic;
using System.Linq;
using Kontur.Courses.Testing.Implementations;
using NUnit.Framework;
using Shouldly;

namespace Kontur.Courses.Testing
{
	[TestFixture]
	public class WordsStatistics_Tests
	{
		public virtual IWordsStatistics CreateStat()
		{
			// меняется на разные реализации при запуске exe
			return new WordsStatistics();
		}

		public IWordsStatistics stat;

		[SetUp]
		public void SetUp()
		{
			stat = CreateStat();
		}

		[Test]
		public void Empty_AfterCreation()
		{
			stat.GetStatistics().ShouldBeEmpty();
		}

		[Test]
		public void SameWord_CountsOnce()
		{
			stat.AddWord("xxxxxxxxxx");
			stat.AddWord("xxxxxxxxxx");
			stat.GetStatistics().Count().ShouldBe(1);
		}

		// See shouldly docs http://docs.shouldly-lib.net/docs/overview
	}

}