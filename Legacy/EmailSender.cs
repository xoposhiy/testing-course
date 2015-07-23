using System;

namespace LegacyCode
{
	public class EmailSender : IEmailSender
	{
		public void SendMessage(EmailMessage message)
		{
			// Тут код, который трудно заставить работать в тестовом окружении.
			throw new Exception("EmailSender is not configured");
		}

		public string MailServer { get { throw new Exception("EmailSender is not configured"); }}
	}
}