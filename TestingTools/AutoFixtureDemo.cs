using System;
using System.Linq;
using Kontur.Courses.Testing;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.Kernel;
using Ploeh.SemanticComparison;
using Shouldly;

//using Shouldly;

namespace TestingTools
{
	[TestFixture]
	public class AutoFixtureDemo
	{
		// https://app.pluralsight.com/library/courses/autofixture-dotnet-unit-test-get-started/table-of-contents

		private ProviderDataProcessor processor;
		private Fixture fixture;

		[SetUp]
		public void SetUp()
		{
			processor = new ProviderDataProcessor();
			fixture = new Fixture();
		}

		[Test]
		public void CommonWay()
		{
			var data = new ProviderData(
				Guid.NewGuid(), DateTime.Now, false,
				new[]
				{
					new ProductData(Guid.NewGuid(), "Name", 1.1m, "items"),
				});

			var report = processor.Process(data);

			report.Success.ShouldBe(true);
		}

		[Test]
		public void AutofixtureWay()
		{
			var data = fixture.Create<ProviderData>();

			var report = processor.Process(data);

			report.Success.ShouldBe(true);
		}

		[Test]
		public void AutofixtureWay_ErrorWhenNoProducts()
		{
			var data = fixture.Build<ProviderData>()
				.With(d => d.Products, new ProductData[0])
				.Create();

			var report = processor.Process(data);

			report.Success.ShouldBe(false);
			report.Error.ShouldBe("Should be products");
		}
	}
}