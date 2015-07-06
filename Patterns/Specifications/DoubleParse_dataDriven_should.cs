using System.Globalization;
using NUnit.Framework;

namespace Kontur.Courses.Testing.Patterns.Specifications
{
	[TestFixture]
	public class DoubleParse_should
	{
		[TestCase("123", Result = 123, TestName = "integer")]
		[TestCase("1.1", Result = 1.1, TestName = "fraction")]
		[TestCase("1.1e1", Result = 1.1e1, TestName="scientific with positive exp")]
		[TestCase("1.1e-1", Result = 1.1e-1, TestName = "scientific with negative exp")]
		[TestCase("-0.1", Result = -0.1, TestName="negative fraction")]
		public double withInvariantCulture_parse(string input)
		{
			return double.Parse(input, CultureInfo.InvariantCulture);
		}
	}
}