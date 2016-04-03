using System;
using System.Linq;
using ApprovalTests;
using ApprovalTests.Core;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using FakeItEasy;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Shouldly;
using StatePrinter;

namespace Legacy.ProviderProcessing
{
	[TestFixture]
	[UseReporter(typeof(DiffReporter))]
	public class ProviderProcessor_Should
	{

		[SetUp]
		public void SetUp()
		{
		}

		[Test]
		public void SaveData_ForNewProvider()
		{
		}

		[Test]
		public void Ignore_OutdatedData()
		{
		}
	}
}