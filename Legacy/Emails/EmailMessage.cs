using System.Text;

namespace Legacy.Emails
{
	public class EmailMessage
	{
		public string Subject;
		public string Body;
		public bool Important;

//		public override string ToString()
//		{
//			return $"Subject: {Subject}, Important: {Important}\r\n{Body}";
//		}
	}

}