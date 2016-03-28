using System;
using ApprovalTests;
using ApprovalTests.Core;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using FakeItEasy;
using NUnit.Framework;
using Ploeh.AutoFixture;
using StatePrinter;

namespace Legacy.ProviderProcessing
{
	[TestFixture]
	[UseReporter(typeof(DiffReporter))]
	public class ProviderProcessor_should
	{
		[Test]
		public void SaveDataForNewProvider()
		{

		}

		[Test]
		public void IgnoreOutdatedData()
		{

		}
	}
}