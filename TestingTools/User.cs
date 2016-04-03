using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace TestingTools
{
	public class User
	{
		public Guid Id;
		public string Name;
		public string PasswordHash;
		public string Salt;

		public static string ComputeHash(string password, string salt)
		{
			var sha256 = SHA256.Create();
			var data = Encoding.UTF8.GetBytes(salt + password);
			return string.Join("", sha256.ComputeHash(data).Select(b => b.ToString("x2")));
		}
	}
}