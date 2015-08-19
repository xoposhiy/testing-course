using System;

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

		public ICommand Replace(int startIndex, int len, string newText)
		{
			var command = new ReplaceCommand(this, startIndex, len, newText);
			command.Do();
			return command;
		}

		class ReplaceCommand : ICommand
		{
			private readonly TextDocument textDocument;
			private readonly int startIndex;
			private readonly int len;
			private readonly string newText;
			private readonly string oldText;
			private bool Done;

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
				if (Done)
					throw new InvalidOperationException("Can't do command because it was done already");
				textDocument.Text = textDocument.Text.Substring(0, startIndex)
					+ newText + textDocument.Text.Substring(startIndex + len);
				textDocument.EditsCount++;
				Done = true;
			}

			public void Undo()
			{
				if (!Done)
					throw new InvalidOperationException("Can't undo command because it was not done yet");
				textDocument.Text = textDocument.Text.Substring(0, startIndex)
					+ oldText + textDocument.Text.Substring(startIndex + newText.Length);
				textDocument.EditsCount--;
				Done = false;
			}
		}
	}


	interface ICommand
	{
		void Do();
		void Undo();
	}
}
