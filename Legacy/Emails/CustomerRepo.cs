using System;

namespace Legacy.Emails
{
	public class CustomerRepo : ICustomerRepo
	{
		public Customer FindById(string customerId)
		{
			throw new Exception("Работа с реальной базой, которой хотелось бы избежать в тестах");
		}
	}
}