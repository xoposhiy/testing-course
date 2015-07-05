using System;

namespace ConwaysGameOfLife
{
	static class Program
	{
		static void Main(string[] args)
		{
			var game = new ConwaysLife(20, 20, new ConsoleUi(20, 20));
			game.SetAge(new Point(5, 0), 1);
			game.SetAge(new Point(6, 1), 1);
			game.SetAge(new Point(7, 1), 1);
			game.SetAge(new Point(6, 2), 1);
			game.SetAge(new Point(5, 2), 1);
			while (true)
			{
				game.Step();
				Console.ReadKey();
			}
		}
	}
}
