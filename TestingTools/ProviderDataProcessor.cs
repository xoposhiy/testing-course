using System.Linq;

namespace TestingTools
{
	public class ProviderDataProcessor
	{
		public ProcessingReport Process(ProviderData data)
		{
			if (data.Products.Any())
				return new ProcessingReport(true, null);
			else
			{
				return new ProcessingReport(false, "Should be products");
			}
		}
	}
}