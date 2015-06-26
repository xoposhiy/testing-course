using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Kontur.Courses.Testing.Patterns.Specifications
{
	public class MarkdownProcessor
	{
		public string Process(string input)
		{
			var emReplacer = new Regex(@"([^\w\\]|^)_(.*?[^\\])_(\W|$)");
			var strongReplacer = new Regex(@"([^\w\\]|^)__(.*?[^\\])__(\W|$)");
			input = strongReplacer.Replace(input, match => match.Groups[1].Value + "<strong>" + match.Groups[2].Value + "</strong>" + match.Groups[3].Value);
			input = emReplacer.Replace(input, match => match.Groups[1].Value + "<em>" + match.Groups[2].Value + "</em>" + match.Groups[3].Value);
			input = input.Replace(@"\_", "_");
			return input;
		}
	}

	[TestFixture]
	public class MarkdownProcessor_should
	{
		private readonly MarkdownProcessor md = new MarkdownProcessor();
		private const string noMarkupText = "text with no markup";

		[Test]
		public void notChangeText_IfNoMarkup()
		{
			var result = md.Process(noMarkupText);
			Assert.AreEqual(noMarkupText, result);
		}

		[TestCase("it _is_ it!", Result = "it <em>is</em> it!")]
		[TestCase("_a_", Result = "<em>a</em>")]
		[TestCase("_a_ b", Result = "<em>a</em> b")]
		[TestCase("b _a_", Result = "b <em>a</em>")]
		public string surroundWithEm_textInsideUnderscores(string input)
		{
			return md.Process(input);
		}

		[TestCase("it __is__ it!", Result = "it <strong>is</strong> it!")]
		[TestCase("__a__", Result = "<strong>a</strong>")]
		[TestCase("__a__ b", Result = "<strong>a</strong> b")]
		[TestCase("b __a__", Result = "b <strong>a</strong>")]
		public string surroundWithStrong_textInsideDoubleUnderscores(string input)
		{
			return md.Process(input);
		}

		[TestCase("a __b _c_ d__ e", Result= "a <strong>b <em>c</em> d</strong> e")]
		[TestCase("__b _c_ d__ e", Result= "<strong>b <em>c</em> d</strong> e")]
		[TestCase("__b _c_ d__", Result= "<strong>b <em>c</em> d</strong>")]
		[TestCase("a __b _c_ d__", Result= "a <strong>b <em>c</em> d</strong>")]
		public string format_EmInsideStrong(string input)
		{
			return md.Process(input);
		}

		[TestCase("a_b_c", Result = "a_b_c")]
		[TestCase("1_2_3", Result = "1_2_3")]
		[TestCase("1_b_3", Result = "1_b_3")]
		public string ignore_UnderscoreInsideWord(string input)
		{
			return md.Process(input);
		}

		[TestCase("_a", Result = "_a")]
		[TestCase("b_", Result = "b_")]
		[TestCase("a _b c", Result = "a _b c")]
		[TestCase("a b_ c", Result = "a b_ c")]
		public string ignore_UnpairedUnderscores(string input)
		{
			return md.Process(input);
		}

		[TestCase(@"\_u_", Result = "_u_")]
		[TestCase(@"_u\_", Result = "_u_")]
		[TestCase(@"\_u\_", Result = "_u_")]
		[TestCase(@"\__u__", Result = "__u__")]
		[TestCase(@"__u\_\_", Result = "__u__")]
		[TestCase(@"\__u\_\_", Result = "__u__")]
		public string ignore_EsacepedUnderscore(string input)
		{
			return md.Process(input);
		}
	}
}
