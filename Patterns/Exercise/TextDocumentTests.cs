using System;
using NUnit.Framework;

namespace Kontur.Courses.Testing.Patterns.Exercise
{
	[TestFixture]
	public class TextDocumentTests
	{
		[Test]
		public void Test()
		{
			var doc = new TextDocument("hello world");
			var cmd = doc.MakeReplaceCommand(0, 1, "H");
			cmd.Do();
			Assert.AreEqual("Hello world", doc.Text);
			var cmd2 = doc.MakeReplaceCommand(6, 5, "all!");
			cmd2.Do();
			Assert.AreEqual("Hello all!", doc.Text);
			Assert.AreEqual(2, doc.EditsCount);
			cmd2.Undo();
			Assert.AreEqual(1, doc.EditsCount);
			cmd.Undo();
			Assert.AreEqual(0, doc.EditsCount);
			Assert.AreEqual("hello world", doc.Text);
			try
			{
				cmd2.Undo();
				Assert.Fail("Should not be able to undo command which was not executed before");
			}
			catch (InvalidOperationException)
			{
				Console.WriteLine("Exception was thrown!");
			}
		}
	}
}