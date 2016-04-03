using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using Newtonsoft.Json;

namespace Legacy.ProviderProcessing
{
	public class ProviderProcessor
	{
		private readonly ProviderRepository repo;

		public ProviderProcessor()
		{
			repo = new ProviderRepository();
		}

		public ProcessReport ProcessProviderData(string message)
		{
			var data = JsonConvert.DeserializeObject<ProviderData>(message);
			var existingData = repo.FindByProviderId(data.ProviderId);
			if (existingData != null && data.Timestamp < existingData.Timestamp)
			{
				log.InfoFormat("Outdated provider data. ProviderId {0} Received timestamp: {1} database timestamp {2}",
					data.ProviderId, data.Timestamp, existingData.Timestamp);
				return new ProcessReport(false, "Outdated data");
			}
			var errors = GetProductValidationErrors(data.Products).ToList();
			if (errors.Any())
			{
				return new ProcessReport(
					false, "Product validation errors",
					errors.Select(e => e.Item2 + ". ProductId " + e.Item1.Id).ToArray());
			}
			if (existingData == null)
			{
				repo.Save(data);
			}
			if (data.ReplaceData)
			{
				log.InfoFormat("Provider {0} products replaced. Deleted: {1} Added: {2}",
					data.ProviderId, existingData.Products.Length, data.Products.Length);
				repo.RemoveById(existingData.Id);
				repo.Save(data);
			}
			else
			{
				var actualProducts = existingData.Products.Where(p => data.Products.All(d => d.Id != p.Id)).ToList();
				var updatedCount = existingData.Products.Length - actualProducts.Count;
				var newCount = data.Products.Length - updatedCount;
				log.InfoFormat("Provider {0} products update. New: {1} Updated: {2}",
					data.ProviderId, newCount, updatedCount);
				existingData.Products = actualProducts.Concat(data.Products).ToArray();
				repo.Update(existingData);
			}
			return new ProcessReport(true, "OK");
		}


		public virtual IEnumerable<Tuple<ProductData, string>> GetProductValidationErrors(ProductData[] data)
		{
			var reference = ProductsReference.GetInstance();
			foreach (var product in data)
			{
				if (!reference.FindCodeByName(product.Name).HasValue)
					yield return Tuple.Create(product, "Unknown product name");
				if (product.Price <= 0 || product.Price > Settings.Global.MaxPossiblePrice)
					yield return Tuple.Create(product, "Bad price");
				if (!IsValidUnitsCode(product.UnitsCode))
					yield return Tuple.Create(product, "Bad units of measurements");
				//...

			}
		}

		private bool IsValidUnitsCode(string unitsCode)
		{
			var reference = UnitsReference.GetInstance();
			return reference.FindByCode(unitsCode) != null;
		}

		private static readonly ILog log = LogManager.GetLogger(typeof(ProviderProcessor));
	}
}