using System;

namespace ConwaysGameOfLife
{
	internal static class Program
	{
		private static void Main(string[] args)
		{
			var ui = new ConsoleUi(20, 20);
			var game = new ConwaysLife(20, 20, ui);
			game.ReviveCells(
				new Point(5, 0), new Point(5, 2),
				new Point(6, 1), new Point(6, 2),
				new Point(7, 1));
			while (true)
			{
				Console.ReadKey();
				game.Step();
			}
		}
	}
}