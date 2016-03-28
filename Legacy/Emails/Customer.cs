namespace Legacy.Emails
{
	public class Customer
	{
		public readonly string Name;
		public readonly AccountType AccountType;

		public Customer(string name, AccountType accountType)
		{
			Name = name;
			AccountType = accountType;
		}
	}
}