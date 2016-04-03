using System;
using System.Text;

namespace Legacy.Emails
{
	public class EmailMessage
	{
		public Guid Id = Guid.NewGuid();
		public string Subject;
		public string Body;
		public bool Important;

		public override string ToString()
		{
			return String.Format("Subject: {0}, Body: {1}, Important: {2}", Subject, Body, Important);
		}
	}

}