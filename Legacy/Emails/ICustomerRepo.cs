namespace Legacy.Emails
{
	public interface ICustomerRepo
	{
		Customer FindById(string customerId);
	}
}