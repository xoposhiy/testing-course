using System.Collections.Generic;
using System.Linq;

namespace ConwaysGameOfLife
{
	public class Game : IReadonlyField
	{
		private readonly int width;
		private readonly int height;
		private readonly IGameUi ui;
		private bool[,] aliveCells;

		public Game(int width, int height, IGameUi ui)
		{
			this.width = width;
			this.height = height;
			this.ui = ui;
			aliveCells = new bool[width, height];
		}

		public bool IsAlive(Point pos)
		{
			return IsAlive(pos.X, pos.Y);
		}

		public bool IsAlive(int x, int y)
		{
			return aliveCells[(x + width) % width, (y + height) % height];
		}

		public void Revive(params Point[] cells)
		{
			foreach (var pos in cells)
				aliveCells[(pos.X + width) % width, (pos.Y + height) % height] = true;
			ui.Update(this);
		}

		public void Step()
		{
			var newCellAge = new bool[width, height];
			for (int y = 0; y < height; y++)
				for (int x = 0; x < width; x++)
				{
					var aliveCount = GetNeighbours(x, y).Count(IsAlive);
					newCellAge[x, y] = WillBeAlive(aliveCells[x, y], aliveCount);
				}
			aliveCells = newCellAge;
			ui.Update(this);
		}

		private bool WillBeAlive(bool isAlive, int aliveNeighbours)
		{
			return aliveNeighbours == 3 || aliveNeighbours == 2 && isAlive;
		}

		private IEnumerable<Point> GetNeighbours(int x, int y)
		{
			return
				from nx in new[] { x - 1, x, x + 1 }
				from ny in new[] { y - 1, y, y + 1 }
				where nx != x || ny != y
				select new Point(nx, ny);
		}

		public override string ToString()
		{

			var rows = Enumerable.Range(0, height)
				.Select(y => 
					string.Join("",
						Enumerable.Range(0, width).Select(x => aliveCells[x, y] ? "#" : " ")
				));
			return string.Join("\n", rows);
		}
	}
}