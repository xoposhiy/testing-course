using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontur.Courses.Testing.Patterns.Exercise
{
	class TextDocument
	{
		public string Text { get; private set; }
		public int EditsCount { get; private set; }

		public TextDocument(string text)
		{
			Text = text;
		}

		public ICommand MakeReplaceCommand(int startIndex, int len, string newText)
		{
			return new ReplaceCommand(this, startIndex, len, newText);
		}

		class ReplaceCommand : ICommand
		{
			private readonly TextDocument textDocument;
			private readonly int startIndex;
			private readonly int len;
			private readonly string newText;
			private readonly string oldText;

			public ReplaceCommand(TextDocument textDocument, int startIndex, int len, string newText)
			{
				this.textDocument = textDocument;
				this.startIndex = startIndex;
				this.len = len;
				this.newText = newText;
				this.oldText = textDocument.Text.Substring(startIndex, len);
			}

			public void Do()
			{
				if (textDocument.Text.Substring(startIndex, len) != oldText)
					throw new InvalidOperationException();
				textDocument.Text = textDocument.Text.Substring(0, startIndex)
					+ newText + textDocument.Text.Substring(startIndex + len);
				textDocument.EditsCount++;
			}

			public void Undo()
			{
				if (textDocument.Text.Substring(startIndex, newText.Length) != newText)
					throw new InvalidOperationException();
				textDocument.Text = textDocument.Text.Substring(0, startIndex)
					+ oldText + textDocument.Text.Substring(startIndex + newText.Length);
				textDocument.EditsCount--;
			}
		}
	}


	interface ICommand
	{
		void Do();
		void Undo();
	}
}
