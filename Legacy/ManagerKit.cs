namespace LegacyCode
{
	public class ManagerKit
	{
		public static readonly Logger Logger = new Logger();
		public static readonly IEmailSender EmailSender = new EmailSender();
		public static readonly LocalConfig Config = new LocalConfig { IncreaseRate = true, IncreaseRateFactor = 1.01m };
	}

	public class LocalConfig
	{
		public bool IncreaseRate { get; set; }
		public decimal IncreaseRateFactor { get; set; }
	}
}