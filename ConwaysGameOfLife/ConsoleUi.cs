using System;

namespace ConwaysGameOfLife
{
	public class ConsoleUi : IGameUi
	{
		public ConsoleUi(int width, int height)
		{
			this.width = width;
			this.height = height;
		}

		private readonly int width;
		private readonly int height;

		public void Update(IReadonlyField field)
		{
			var oldColor = Console.BackgroundColor;
			try
			{
				Console.SetCursorPosition(0, 0);
				for (int y = 0; y < height; y++)
				{
					for (int x = 0; x < width; x++)
					{
						var alive = field.IsAlive(x, y);
						Console.BackgroundColor = alive ? ConsoleColor.Yellow : ConsoleColor.Black;
						Console.Write(' ');
					}
					Console.WriteLine();
				}
			}
			finally
			{
				Console.BackgroundColor = oldColor;
			}
		}
	}
}