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
		public Func<IWordsStatistics> createStat = () => new WordsStatistics();
			// меняется на разные реализации при запуске exe

		public IWordsStatistics stat;

		[SetUp]
		public void SetUp()
		{
			stat = createStat();
		}

		[Test]
		public void Empty_AfterCreation()
		{
			stat.GetStatistics().ShouldBeEmpty();
		}

		[Test]
		public void SameWord_CountsOnce()
		{
			stat.AddWord("xxxxxxxxxxx");
			stat.AddWord("xxxxxxxxxxx");
			stat.GetStatistics().Count().ShouldBe(1);
			
		}
		// See shouldly docs http://docs.shouldly-lib.net/docs/overview
	}

}