using System;
using ApprovalTests;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using NUnit.Framework;

namespace Legacy
{
	public class PairwiseDemo
	{
		[Test, Combinatorial]
		public void CombinatorialConsole(
			[Values("a", "b", "c")] string a,
			[Values("+", "-")] string b,
			[Values("x", "y")] string c)
		{
			Console.WriteLine("{0} {1} {2}", a, b, c);
		}

		[Test, Pairwise]
		public void PairwiseConsole(
			[Values("a", "b", "c")] string a,
			[Values("+", "-")] string b,
			[Values("x", "y")] string c)
		{
			Console.WriteLine("{0} {1} {2}", a, b, c);
		}

		[Test, Pairwise]
		[UseReporter(typeof(DiffReporter))]
		public void PairwiseApprovals(
			[Values("a", "b", "c")] string a,
			[Values("+", "-")] string b,
			[Values("x", "y")] string c)
		{
			NamerFactory.AdditionalInformation = a + b + c;
			var v = $"{a} {b} {c}";
			Approvals.Verify(v);
		}
	}
}