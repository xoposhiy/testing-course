using System;
using System.Linq;
using Kontur.Courses.Testing.Implementations;
using NUnit.Framework;

namespace Kontur.Courses.Testing
{
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
		public void no_stats_if_no_words()
		{
			CollectionAssert.IsEmpty(stat.GetStatistics());
		}

		[Test]
		public void same_word_twice()
		{
			stat.AddWord("xxxxxxxxxxx");
			stat.AddWord("xxxxxxxxxxx");
			Assert.AreEqual(1, stat.GetStatistics().Count());
		}

		[Test]
		public void FailsOnNull()
		{
			Assert.Throws<ArgumentNullException>(() => stat.AddWord(null));
		}

		[Test]
		public void IgnoreEmpty()
		{
			stat.AddWord("");
			Assert.AreEqual(0, stat.GetStatistics().Count());
		}
		[Test]
		public void AddLeadingSpaces()
		{
			stat.AddWord("          a");
			Assert.AreEqual(1, stat.GetStatistics().Count());
		}
		[Test]
		public void Trim10Letters()
		{
			stat.AddWord("12345678901234");
			Assert.AreEqual(new[] { Tuple.Create(1, "1234567890") }, stat.GetStatistics());
		}
		[Test]
		public void Trim10Letters2()
		{
			stat.AddWord("12345678901");
			Assert.AreEqual(new[] { Tuple.Create(1, "1234567890") }, stat.GetStatistics());
		}
		[Test]
		public void DontTrim9Letters()
		{
			stat.AddWord("123456789");
			Assert.AreEqual(new[] { Tuple.Create(1, "123456789") }, stat.GetStatistics());
		}
		[Test]
		public void DontTrim2Letters()
		{
			stat.AddWord("12");
			Assert.AreEqual(new[] { Tuple.Create(1, "12") }, stat.GetStatistics());
		}
		[Test]
		public void IgnoreWritespace()
		{
			stat.AddWord("  ");
			Assert.IsEmpty(stat.GetStatistics());
		}

		[Test]
		public void OrderByFreq()
		{
			stat.AddWord("x");
			stat.AddWord("x");
			stat.AddWord("c");
			stat.AddWord("b");
			stat.AddWord("d");
			Assert.AreEqual(new[] {Tuple.Create(2, "x"), Tuple.Create(1, "b"), Tuple.Create(1, "c"), Tuple.Create(1, "d") }, stat.GetStatistics());
		}

		[Test]
		public void CaseToLower()
		{
			stat.AddWord("A");
			stat.AddWord("a");
			Assert.AreEqual(new[] { Tuple.Create(2, "a")}, stat.GetStatistics());
		}

		[Test]
		public void TwoInstances()
		{
			stat.AddWord("a");
			var s2 = createStat();
			s2.AddWord("b");
			Assert.AreEqual(new[] { Tuple.Create(1, "a") }, stat.GetStatistics());
		}

		[Test]
		[Timeout(1000)]
		public void LargeTest()
		{
			for (int i = 0; i < 10000; i++)
			{
				stat.AddWord(i.ToString());
				stat.AddWord(i.ToString());
			}
			Assert.AreEqual(10000, stat.GetStatistics().Count());
			//CollectionAssert.AreEquivalent(Enumerable.Range(0, 2000).Select(i => Tuple.Create(1, i.ToString())), stat.GetStatistics().ToList());
		}

		[Test]
		public void StrangeLetters()
		{
			stat.AddWord("Ё");
			Assert.AreEqual(new[] {Tuple.Create(1, "ё")}, stat.GetStatistics());
			//CollectionAssert.AreEquivalent(Enumerable.Range(0, 2000).Select(i => Tuple.Create(1, i.ToString())), stat.GetStatistics().ToList());
		}
	}
}