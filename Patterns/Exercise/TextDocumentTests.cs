using System;
using NUnit.Framework;

namespace Kontur.Courses.Testing.Patterns.Exercise
{
	[TestFixture]
	public class TextDocumentTests
	{
		private const string DefaultText = "hello world";
		private TextDocument doc;

		[SetUp]
		public void SetUp()
		{
			doc = new TextDocument(DefaultText);
		}

		[Test]
		public void TestDo()
		{
			doc.Replace(0, 1, "H");
			Assert.AreEqual("Hello world", doc.Text);
		}

		[Test]
		public void TestMoreComplexDo()
		{
			doc.Replace(0, 1, "H");
			doc.Replace(0, 5, "hi");
			doc.Replace(3, 5, "all");
			Assert.AreEqual("hi all", doc.Text);
			Assert.AreEqual(3, doc.EditsCount);
		}

		[Test]
		public void TestUndo()
		{
			var command = doc.Replace(0, 1, "H");
			command.Undo();
			Assert.AreEqual(DefaultText.ToLower(), doc.Text);
			Assert.AreEqual(0, doc.EditsCount);
			try
			{
				command.Undo();
				Assert.Fail("Can't undo twice");
			}
			catch (InvalidOperationException)
			{
			}
		}
	}
}