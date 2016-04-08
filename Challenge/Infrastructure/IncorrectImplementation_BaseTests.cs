using System;
using Kontur.Courses.Testing.Implementations;
using NUnit.Framework;

namespace Kontur.Courses.Testing.Infrastructure
{
	public abstract class IncorrectImplementation_BaseTests : WordsStatistics_Tests
	{
		public override IWordsStatistics CreateStat()
		{
			string ns = typeof(WordsStatisticsL).Namespace;
			var implTypeName = ns + "." + GetType().Name.Replace("_Tests", "");
			var implType = GetType().Assembly.GetType(implTypeName);
			if (implType == null)
				Assert.Fail("no type {0}", implTypeName);
			return (IWordsStatistics) Activator.CreateInstance(implType);
		}
	}
}

