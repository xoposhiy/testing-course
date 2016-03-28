using System.Text;

namespace Legacy.Emails
{
	public class NewRateEmailMessager
	{
		private readonly CustomerRepo repo;
		private readonly EmailSender emailSender;

		public NewRateEmailMessager(CustomerRepo repo, EmailSender emailSender)
		{
			this.repo = repo;
			this.emailSender = emailSender;
		}

		public void SendMessage(string customerId, decimal newRate)
		{
			var message = new EmailMessage();
			Customer customer = repo.FindById(customerId);

			message.Subject = "New rate!";
			var sb = new StringBuilder();


			sb.AppendFormatLine("Dear {0}", customer.Name);
			sb.AppendLine();

			sb.Append("We are sending you this message with respect to your ");
			switch (customer.AccountType)
			{
				case AccountType.Cheque:
					sb.Append("chequing account");
					break;
				case AccountType.Savings:
					sb.Append("on line savings account");
					break;
				case AccountType.Credit:
					sb.Append("credit card");
					break;
			}

			sb.AppendLine();
			sb.AppendLine();


			switch (customer.AccountType)
			{
				case AccountType.Cheque:
					sb.AppendFormatLine("The interest rate at which you earn interest has changed to {0}%.",
						newRate);
					break;
				case AccountType.Savings:
					sb.AppendFormatLine("Your savings interest rate has changed to {0}%", newRate);
					break;
				case AccountType.Credit:
					sb.AppendFormatLine(
						"The interest rate for which you will be charged for new purchases is now {0}%", newRate);
					break;
			}
			if (newRate > 0.1m)
				message.Important = true;


			sb.AppendLine();
			sb.AppendLine();

			sb.AppendLine("Kind regards - your bank.");
			message.Body = sb.ToString();
			emailSender.SendMessage(message);
		}
	}

}
