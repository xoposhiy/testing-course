using NUnit.Framework;

namespace ConwaysGameOfLife
{
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