using System;

namespace ConwaysGameOfLife
{
	internal static class Program
	{
		private static void Main()
		{
			var ui = new ConsoleUi(60, 20);
			var game = new Game(60, 20, ui);
			game.Revive(Patterns.GetGlider(new Point(25, 8)));
			while (true)
			{
				var key = Console.ReadKey(intercept:true);
				if (key.Key == ConsoleKey.Escape) break;
				game.Step();
			}
		}
	}
}