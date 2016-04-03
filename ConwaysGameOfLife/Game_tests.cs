using System.Linq;
using NUnit.Framework;

namespace ConwaysGameOfLife
{
	public class Game_tests
	{
		[Test]
		public void singleCell_dissapear_afterStep()
		{
			var fakeUi = new FakeUi();
			// fakeUi = A.Fake<...>();
			var game = new Game(10, 10, fakeUi);
			game.Revive(new Point(1, 1));

			game.Step();

			Assert.IsFalse(game.IsAlive(1, 1));
		}

		[Test]
		public void Revive_UpdatesAllUi()
		{
			//TODO
		}

		[Test]
		public void Step_UpdatesParticularCellsInUi()
		{
			//TODO
		}

		// TODO: Избавиться от этого класса в пользу библиотеки FakeItEasy
		public class FakeUi : IGameUi
		{
			public void Update(IReadonlyField field)
			{
			}
		}
	}
}