using FakeItEasy;
using NUnit.Framework;
using Shouldly;

namespace TestingTools
{
	[TestFixture]
	public class FakeItEasyDemo
	{
		// https://app.pluralsight.com/library/courses/fakeiteasy
		public class UserManager_Create_Should
		{
			[Test]
			[Explicit]
			public void SaveNewUserToUserRepo_WhenEverythingIsValid()
			{
				//Arrange
				var userManager = new UserManager(null /*?!?*/);

				//Act
				userManager.CreateUser("pe", "qweqwe");

				//Assert ?!?
			}

			#region Solved
			[Test]
			public void SaveNewUserToUserRepo_WhenEverythingIsValid_Solved()
			{
				//Arrange
				var userRepo = A.Fake<IUserRepo>();
				A.CallTo(() => userRepo.FindByName(A<string>.Ignored)).Returns(null);
				var userManager = new UserManager(userRepo);

				//Act
				userManager.CreateUser("pe", "qweqwe");

				//Assert
				A.CallTo(() => userRepo.Insert(A<User>.Ignored)).MustHaveHappened(); // не проверяет созданного пользователя

				// можно было сделать как-то так:
				A.CallTo(() =>
					userRepo.Insert(A<User>.That.Matches(u => u.PasswordHash == User.ComputeHash("qweqwe", u.Salt)))
					).MustHaveHappened();
				// но лучше оставить проверку корректности заполенния полей user другим тестом. См ниже.
			}

			[Test]
			public void FillInUserNameAndPassword()
			{
				var user = UserManager.CreateNewUser("pe", "qweqwe");
				user.Name.ShouldBe("pe");
				user.PasswordHash.ShouldBe(User.ComputeHash("qweqwe", user.Salt));
			}

			[Test]
			public void FillInUserSaltWithUniqueValues()
			{
				var user1 = UserManager.CreateNewUser("pe1", "qweqwe");
				var user2 = UserManager.CreateNewUser("pe2", "qweqwe");
				user2.Salt.ShouldNotBe(user1.Salt);
			}
			#endregion
		}
	}
}