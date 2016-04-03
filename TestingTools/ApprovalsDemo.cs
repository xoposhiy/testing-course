using System;
using ApprovalTests;
using ApprovalTests.Maintenance;
using ApprovalTests.Reporters;
using NUnit.Framework;
using Ploeh.AutoFixture;
using StatePrinter;

namespace TestingTools
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
			var data = new ProductData(Guid.Empty, "Name", 3.14m, "items");

			Approvals.Verify(new Stateprinter().PrintObject(data)); //Stateprinter, not StatePrinter!
		}


		[Test]
		public void Maintainance()
		{
			ApprovalMaintenance.VerifyNoAbandonedFiles();
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
				.Exclude<ProviderData>(d => d.ProviderId, d => d.Timestamp)
				.Exclude<ProductData>(d => d.Id);
			Approvals.Verify(printer.PrintObject(data));
		}

	}
}