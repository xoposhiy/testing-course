using System.Linq;
using NUnit.Framework;

namespace ConwaysGameOfLife
{
	public class PatternsMother
	{
		public Point[] GetHorizontalStick(Point topLeft)
		{
			return new[] { P(0, 0), P(1, 0), P(2, 0) }.Select(p => p.Add(topLeft)).ToArray();
		}

		public Point[] GetR(Point topLeft)
		{
			return new[] { 
							P(1, 0),	P(2, 0), 
				P(0, 1),	P(1, 1), 
							P(1, 2)
			}.Select(p => p.Add(topLeft)).ToArray();
		}
		private static Point P(int x, int y)
		{
			return new Point(x, y);
		}
	}

	public class ConwaysLife_tests
	{
		[Test]
		public void singleCell_dissapear_afterStep()
		{
			var game = new ConwaysLife(10, 10, new FakeUi());
			game.ReviveCells(new Point(1, 1));

			game.Step();

			Assert.AreEqual(0, game.GetAge(new Point(1, 1)));
		}

		public class FakeUi : IGameUi
		{
			public void Update(IReadonlyField field)
			{
			}
		}
	}
}