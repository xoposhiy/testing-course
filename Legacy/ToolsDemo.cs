using System;
using System.Collections.Generic;
using System.Linq;
using ApprovalTests;
using ApprovalTests.Reporters;
using Legacy.ProviderProcessing;
using NUnit.Framework;
using Ploeh.AutoFixture;
using StatePrinter;

namespace Legacy
{
	[TestFixture]
	public class ToolsDemo
	{
		[Test]
		[UseReporter(typeof(DiffReporter))]
		public void TestStringConstructor()
		{
			var s = new string('v', 10);

			Approvals.Verify(s);
		}

		[Test]
		[UseReporter(typeof (DiffReporter))]
		public void ApproveProductDataWithStateprinter()
		{
			var d = new ProductData { Id = Guid.Empty, Name = "Name", Price = 3.14m, UnitsCode = "112" };

			Approvals.Verify(new Stateprinter().PrintObject(d)); //Stateprinter, not StatePrinter!
		}

		[Test]
		public void GenerateDataWithAutoFixture()
		{
			var fixture = new Fixture();
			var data = fixture.Build<ProductData>()
				.Without(d => d.Id)
				.Create();
			Assert.AreEqual(Guid.Empty, data.Id);
		}


		[Test]
		[UseReporter(typeof (DiffReporter))]
		public void CreateConstantDataWithAutofixture()
		{
			var fixture = new Fixture();
			var r = new Random(12345);
			fixture.Register(() => r.Next().ToString());
			fixture.Register(() => (decimal) r.Next());
			var data = fixture.Create<ProviderData>();


			var printer = new Stateprinter();
			printer.Configuration.Project
				.Exclude<ProviderData>(d => d.Id, d => d.ProviderId, d => d.Timestamp)
				.Exclude<ProductData>(d => d.Id);
			Approvals.Verify(printer.PrintObject(data));
		}

	}
}