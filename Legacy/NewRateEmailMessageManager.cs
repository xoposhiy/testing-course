using System.Text;

namespace LegacyCode
{
	public class NewRateEmailMessageManager
	{
		public string CreateMessageBody(string customerName, AccountType accountType,
			string accountNumber, bool showAccountNumber, decimal newRate)
		{
			var sb = new StringBuilder();

			sb.AppendFormatLine("Dear {0}", customerName);
			sb.AppendLine();

			sb.Append("We are sending you this message with respect to your ");
			switch (accountType)
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

			if (showAccountNumber)
			{
				sb.AppendFormatLine(" with the account number {0}.", accountNumber);
			}
			else
			{
				sb.AppendLine(".");
			}

			sb.AppendLine();
			sb.AppendLine();


			switch (accountType)
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


			sb.AppendLine();
			sb.AppendLine();

			sb.AppendLine("Kind regards - your bank.");

			return sb.ToString();
		}
	}
}
