using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
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
			if (TestsAreValid())
			{
				var incorrectImplementations = GetIncorrectImplementations();
				CheckImplementationsFail(incorrectImplementations);
			}
		}

		private static void CheckImplementationsFail(IEnumerable<Type> implementations)
		{
			foreach (var implementation in implementations)
			{
				var failed = GetFailedTests(implementation, false).ToList();
				Console.Write(implementation.Name + "\t");
				if (failed.Any())
				{
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine("fails on tests: " + string.Join(", ", failed));
					Console.ForegroundColor = ConsoleColor.Gray;
				}
				else
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("you should write test to kill this incorrect implementation");
					Console.ForegroundColor = ConsoleColor.Gray;
				}
			}
		}

		private static IEnumerable<Type> GetIncorrectImplementations()
		{
			return
				Assembly.GetExecutingAssembly().GetTypes()
					.Where(typeof (IWordsStatistics).IsAssignableFrom)
					.Where(t => !t.IsAbstract && !t.IsInterface)
					.Where(t => t != typeof (WordsStatistics));
		}

		private static bool TestsAreValid()
		{
			Console.WriteLine("Check all tests pass with correct implementation...");
			var failed = GetFailedTests(typeof (WordsStatistics), true).ToList();
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

	    private static IEnumerable<string> GetFailedTests(Type implementationType, bool printError)
	    {
            var assemblyFileName = $"{implementationType.Name}-{Guid.NewGuid()}.dll";
            var parameters = new CompilerParameters
            {
                GenerateExecutable = false,
                OutputAssembly = assemblyFileName,
            };
	        parameters.ReferencedAssemblies.Add("System.dll");
	        parameters.ReferencedAssemblies.Add("System.Core.dll");
	        parameters.ReferencedAssemblies.Add("Shouldly.dll");
	        parameters.ReferencedAssemblies.Add("nunit.framework.dll");

            var iterfaceSource = File.ReadAllText("..\\..\\IWordsStatistics.cs");
	        var badImplsSource = File.ReadAllText("..\\..\\Implementations\\DoNotOpen.cs");
	        var correctImplSource = File.ReadAllText("..\\..\\Implementations\\WordsStatistics.cs");
	        var testsSource = File.ReadAllText("..\\..\\WordsStatistics_Tests.cs");
	        testsSource = testsSource.Replace("public Func<IWordsStatistics> createStat = () => new WordsStatistics();",
                                             $"public Func<IWordsStatistics> createStat = () => new {implementationType.Name}();");

            var compileResult = CodeDomProvider.CreateProvider("CSharp").CompileAssemblyFromSource(parameters, iterfaceSource, badImplsSource, correctImplSource, testsSource);
	        if (compileResult.Errors.HasErrors)
	        {
                var compilerOutput = new StringBuilder();
                foreach (var s in compileResult.Output)
                    compilerOutput.AppendLine(s);
                throw new InvalidOperationException($"Compilation failed: {compilerOutput}");
	        }

            using (var testEngine = TestEngineActivator.CreateInstance())
	        {
	            var testRunner = testEngine.GetRunner(new TestPackage(assemblyFileName));
                var emptyFilter = testEngine.Services.GetService<ITestFilterService>().GetTestFilterBuilder().GetFilter();
                var report = testRunner.Run(null, emptyFilter);
                File.WriteAllText($"{assemblyFileName}.nunitReport.xml", report.OuterXml);
                foreach (var failedTestCase in report.SelectNodes("//test-case[@result='Failed']"))
	                yield return ((XmlNode) failedTestCase).Attributes["methodname"].Value;
	        }
        }
	}
}