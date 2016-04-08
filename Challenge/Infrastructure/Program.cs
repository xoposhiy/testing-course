using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using Kontur.Courses.Testing.Implementations;
using NUnit.Engine;

namespace Kontur.Courses.Testing
{
	internal class Program
	{
		private static void Main()
		{
			var testPackage = new TestPackage(Assembly.GetExecutingAssembly().Location);
			using (var engine = new TestEngine())
			using (var testRunner = engine.GetRunner(testPackage))
				if (TestsAreValid(testRunner))
				{
					var incorrectImplementations = ChallengeHelpers.GetIncorrectImplementations();
					CheckImplementationsFail(testRunner, incorrectImplementations);
				}
		}

		private static void CheckImplementationsFail(ITestRunner testRunner, IEnumerable<Type> implementations)
		{
			foreach (var implementation in implementations)
			{
				var failed = GetFailedTests(testRunner, implementation, false).ToList();
				var name = implementation.Name.PadRight(20, ' ');
				if (failed.Any())
				{
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine(TrimToConsole(name + "fails on: " + string.Join(", ", failed)));
					Console.ForegroundColor = ConsoleColor.Gray;
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine(name + "write some test to kill it");
					Console.ForegroundColor = ConsoleColor.Gray;
				}
			}
		}

		private static string TrimToConsole(string text)
		{
			try
			{
				var w = Console.WindowWidth-1;
				if (text.Length > w) return text.Substring(0, w);
				else return text;
			}
			catch (IOException)
			{
				return text;
			}
		}

		private static bool TestsAreValid(ITestRunner testRunner)
		{
			Console.WriteLine("Check all tests pass with correct implementation...");
			var failed = GetFailedTests(testRunner, typeof(WordsStatistics), true).ToList();
			if (failed.Any())
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("Incorrect tests detected: " + string.Join(", ", failed));
				Console.ForegroundColor = ConsoleColor.Gray;
				return false;
			}
			else
			{
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Tests are OK!");
				Console.ForegroundColor = ConsoleColor.Gray;
				return true;
			}
		}

		private static IEnumerable<string> GetFailedTests(ITestRunner testRunner, Type implementationType, bool printError)
		{
			var builder = new TestFilterBuilder();
			builder.AddTest(implementationType.FullName + "_Tests");
			var report = testRunner.Run(null, builder.GetFilter());
			Debug.Assert(report != null);
			File.WriteAllText(String.Format("{0}.nunitReport.xml", implementationType.Name), report.OuterXml);
			var failedTestCases = report.SelectNodes("//test-case[@result='Failed']");
			Debug.Assert(failedTestCases != null);
			foreach (var xmlNode in failedTestCases.Cast<XmlNode>())
			{
				Debug.Assert(xmlNode.Attributes != null);
				yield return xmlNode.Attributes["name"].Value;
			}
		}
	}

	public class ChallengeHelpers
	{
		public static IEnumerable<Type> GetIncorrectImplementations()
		{
			return
				Assembly.GetExecutingAssembly().GetTypes()
					.Where(typeof(IWordsStatistics).IsAssignableFrom)
					.Where(t => !t.IsAbstract && !t.IsInterface)
					.Where(t => t != typeof(WordsStatistics))
					.OrderBy(t => t.Name.Length).ThenBy(t => t.Name);
		}

	}
}