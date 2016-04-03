using System;

namespace TestingTools
{
	public class ProviderData
	{
		public ProviderData(Guid providerId, DateTime timestamp, bool replaceData, ProductData[] products)
		{
			ProviderId = providerId;
			Timestamp = timestamp;
			ReplaceData = replaceData;
			Products = products;
		}

		public Guid ProviderId { get; }
		public DateTime Timestamp { get; }
		public bool ReplaceData { get; set; }
		public ProductData[] Products { get; set; }
	}
}