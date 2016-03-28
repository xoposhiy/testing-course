namespace Legacy.ProviderProcessing
{
	public class Settings
	{
		public static readonly Settings Global = new Settings();
		public bool ReadonlyMode { get; set; }
		public decimal MaxPossiblePrice { get; set; }

		// Много других настроек
	}
}