using System;
using System.Collections.Generic;
using System.Linq;
using DeepEqual;
using DeepEqual.Syntax;
using NUnit.Framework;
using Shouldly;
using StatePrinter;

namespace TestingTools
{
	[TestFixture]
	[Explicit]
	public class DeepEqualDemo
	{
		private ProviderData data1;
		private ProviderData data2;

		[SetUp]
		public void SetUp()
		{
			data1 = new ProviderData(
				Guid.NewGuid(), DateTime.Now, false,
				new[]
				{
					new ProductData(Guid.NewGuid(), "Name", 1.1m, "items"),
				});
			data2 = new ProviderData(
				Guid.NewGuid(), DateTime.MinValue, false,
				new[]
				{
					new ProductData(Guid.NewGuid(), "Name", 1.1m, "items"),
				});
		}

		[Test]
		public void WithDeepEqual_Assert()
		{
			data1.WithDeepEqual(data2)
				.Assert();

		}
		[Test]
		public void ShouldDeepEqual_ComparisonBuilder()
		{
			Console.WriteLine(data1.IsDeepEqual(data2, ProviderDataComparison));
			data1.ShouldDeepEqual(data2, ProviderDataComparison);
		}

		private CompositeComparison ProviderDataComparison
		{
			get
			{
				return new ComparisonBuilder()
					.Create();
			}
		}

		[Test]
		public void ComparisonResult()
		{
			var context = new ComparisonContext();

			var res = ProviderDataComparison.Compare(context, data1, data2);

			Console.WriteLine(new Stateprinter().PrintObject(context));
			res.ShouldBe(DeepEqual.ComparisonResult.Pass);
		}
	}
}