using System;
using System.Linq;
using System.Threading.Tasks;
using Kontur.Courses.Testing;
using NUnit.Framework;
using Shouldly;

namespace TestingTools
{
	[TestFixture]
	[Explicit]
	public class ShouldlyDemo
	{
		// https://app.pluralsight.com/library/courses/shouldly-unit-test-assertions/table-of-contents

		[Test]
		public void Format_zero()
		{
			Assert.AreEqual(new Formatter().Format(0.0), "FREE!");
		}
		[Test]
		public void Format_zero_with_Shouldly()
		{
			new Formatter().Format(0.0).ShouldBe("FREE!");
		}
		[Test]
		public void ApiDemo()
		{
			new[] {1,2,3}.ShouldNotBeEmpty();
			new[] {1,2,3}.ShouldContain(3);
			new[] {1,2,3}.ShouldAllBe(n => n > 0);
			new[] {1,2,3}.ShouldBeSubsetOf(new[] {0,1,3,2,5});
			"Hello World".ShouldContain("hello", Case.Insensitive);
			"123.4".ShouldMatch(@"\d+\.\d+");

			Should.Throw<ArgumentNullException>(() => MethodUnderTest(null)) //returns exception
				.Message.ShouldContain("arg");
		}

		private void MethodUnderTest(object arg)
		{
			if (arg == null) throw new ArgumentNullException(nameof(arg));
		}
	}
}