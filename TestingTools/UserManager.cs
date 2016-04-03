using System;

namespace TestingTools
{
	public class UserManager
	{
		private readonly IUserRepo repo;

		public UserManager(IUserRepo repo)
		{
			this.repo = repo;
		}

		public void CreateUser(string name, string password)
		{
			if (password.Length < 6) throw new Exception("Password is too short");
			if (name.Length < 2) throw new Exception("Name is too short");
			if (repo.FindByName(name) != null)
				throw new Exception("Name is already used");
			var createdUser = CreateNewUser(name, password);
			repo.Insert(createdUser);
		}

		public static User CreateNewUser(string name, string password)
		{
			var salt = Guid.NewGuid().ToString();
			return new User
			{
				Id = Guid.NewGuid(),
				Name = name,
				PasswordHash = User.ComputeHash(password, salt),
				Salt = salt
			};
		}
	}
}