using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Kontur.Courses.Testing.Tdd
{
	public class Game
	{
		public void Roll(int pins)
		{
			
		}

		public int GetScore()
		{
			return 0;
		}
	}

	[TestFixture]
	public class BowlingGame_GetScore_should
	{
		[Test]
		public void returnZero_beforeAnyRolls()
		{
			//TODO
		}
	}
}
