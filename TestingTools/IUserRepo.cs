namespace TestingTools
{
	public interface IUserRepo
	{
		User FindByName(string name);
		void Insert(User created);
	}
}