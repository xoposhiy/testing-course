using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace Kontur.Courses.Testing.Patterns.Specifications
{
	public class MarkdownProcessor
	{
		public string Render(string input)
		{
			var emReplacer = new Regex(@"([^\w\\]|^)_(.*?[^\\])_(\W|$)");
			var strongReplacer = new Regex(@"([^\w\\]|^)__(.*?[^\\])__(\W|$)");
			input = strongReplacer.Replace(input, 
				match => match.Groups[1].Value + 
					"<strong>" + match.Groups[2].Value + "</strong>" + 
					match.Groups[3].Value);
			input = emReplacer.Replace(input, 
				match => match.Groups[1].Value + 
					"<em>" + match.Groups[2].Value + "</em>" + 
					match.Groups[3].Value);
			input = input.Replace(@"\_", "_");
			return input;
		}
	}

	[TestFixture]
	public class MarkdownProcessor_should
	{
		private readonly MarkdownProcessor md = new MarkdownProcessor();

		[Test]
		public void notChangeText_IfNoMarkup()
		{
			var result = md.Render("text with no markup");
			Assert.AreEqual("text with no markup", result);
		}

		[TestCase("it _is_ it!", Result = "it <em>is</em> it!")]
		[TestCase("_a_", Result = "<em>a</em>")]
		[TestCase("_a_ b", Result = "<em>a</em> b")]
		[TestCase("b _a_", Result = "b <em>a</em>")]
		public string surroundWithEm_textInsideUnderscores(string input)
		{
			return md.Render(input);
		}

		
	}
}
