using System;
using System.Linq;
using System.Reflection;
using Kontur.Courses.Testing.Infrastructure;
using NUnit.Framework;

// ReSharper disable once CheckNamespace
namespace Kontur.Courses.Testing
{
	[TestFixture]
	public class GenerateIncorrectTests
	{
		[Test]
		public void Generate()
		{
			var impls = ChallengeHelpers.GetIncorrectImplementations();
			var code = string.Join(Environment.NewLine,
				impls.Select(imp => string.Format("public class {0}_Tests : IncorrectImplementation_BaseTests {{}}", imp.Name))
				);
			Console.WriteLine(code);
		}
		[Test]
		public void CheckAllTestsAreInPlace()
		{
			var impls = ChallengeHelpers.GetIncorrectImplementations();
			foreach (var impl in impls)
			{
				var testName = impl.FullName + "_Tests";
				Assert.NotNull(Assembly.GetExecutingAssembly().GetType(testName), testName + " not found. Regenerate tests with test above!");
			}
		}
	}


	// Generated with test above
	namespace Implementations
	{
		public class WordsStatisticsC_Tests : IncorrectImplementation_BaseTests { }
		public class WordsStatisticsE_Tests : IncorrectImplementation_BaseTests { }
		public class WordsStatisticsL_Tests : IncorrectImplementation_BaseTests { }
		public class WordsStatisticsCR_Tests : IncorrectImplementation_BaseTests { }
		public class WordsStatisticsE2_Tests : IncorrectImplementation_BaseTests { }
		public class WordsStatisticsE3_Tests : IncorrectImplementation_BaseTests { }
		public class WordsStatisticsE4_Tests : IncorrectImplementation_BaseTests { }
		public class WordsStatisticsL2_Tests : IncorrectImplementation_BaseTests { }
		public class WordsStatisticsL3_Tests : IncorrectImplementation_BaseTests { }
		public class WordsStatisticsL4_Tests : IncorrectImplementation_BaseTests { }
		public class WordsStatisticsO1_Tests : IncorrectImplementation_BaseTests { }
		public class WordsStatisticsO2_Tests : IncorrectImplementation_BaseTests { }
		public class WordsStatisticsO3_Tests : IncorrectImplementation_BaseTests { }
		public class WordsStatisticsO4_Tests : IncorrectImplementation_BaseTests { }
		public class WordsStatisticsO5_Tests : IncorrectImplementation_BaseTests { }
		public class WordsStatistics_123_Tests : IncorrectImplementation_BaseTests { }
		public class WordsStatistics_998_Tests : IncorrectImplementation_BaseTests { }
		public class WordsStatistics_999_Tests : IncorrectImplementation_BaseTests { }
		public class WordsStatistics_QWE_Tests : IncorrectImplementation_BaseTests { }
		public class WordsStatistics_STA_Tests : IncorrectImplementation_BaseTests { }
	}
}