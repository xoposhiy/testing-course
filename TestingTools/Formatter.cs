using System;

namespace Kontur.Courses.Testing
{
	public class Formatter
	{
		public string Format(double price)
		{

			return String.Format("Costs ${0}", price);
		}
	}
}