using System.Text;

namespace LegacyCode
{
	public class EmailMessage
	{
		public string Subject;
		public StringBuilder Body = new StringBuilder();
		public bool Important;
	}

	public interface IEmailSender
	{
		void SendMessage(EmailMessage message);
	}
}