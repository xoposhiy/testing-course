using System.Collections.Generic;
using System.Linq;

namespace ConwaysGameOfLife
{
	public class ConwaysLife : IReadonlyField
	{
		private readonly int width;
		private readonly int height;
		private readonly IGameUi ui;
		private int[,] cellAge;

		public ConwaysLife(int width, int height, IGameUi ui)
		{
			this.width = width;
			this.height = height;
			this.ui = ui;
			cellAge = new int[width, height];
		}

		public int GetAge(Point pos)
		{
			return cellAge[(pos.X + width) % width, (pos.Y + height) % height];
		}

		public int GetAge(int x, int y)
		{
			return cellAge[(x + width) % width, (y + height) % height];
		}

		public void SetAge(Point pos, int isAlive)
		{
			cellAge[(pos.X + width) % width, (pos.Y + height) % height] = isAlive;
		}

		public void Step()
		{
			int[,] newCellAge = new int[width, height];
			for (int y = 0; y < height; y++)
				for (int x = 0; x < width; x++)
				{
					var aliveCount = GetNeighbours(x, y).Count(p => GetAge(p) > 0);
					newCellAge[x, y] = NewCellAge(cellAge[x, y], aliveCount);
				}
			cellAge = newCellAge;
			ui.Update(this);
		}

		private int NewCellAge(int age, int aliveNeighbours)
		{
			var willBeAlive = aliveNeighbours == 3 || aliveNeighbours == 2 && age > 0;
			return willBeAlive ? age + 1 : 0;
		}

		private IEnumerable<Point> GetNeighbours(int x, int y)
		{
			yield return new Point(x - 1, y - 1);
			yield return new Point(x - 1, y);
			yield return new Point(x - 1, y + 1);
			yield return new Point(x, y - 1);
			yield return new Point(x, y + 1);
			yield return new Point(x + 1, y - 1);
			yield return new Point(x + 1, y);
			yield return new Point(x + 1, y + 1);
		}
	}
}