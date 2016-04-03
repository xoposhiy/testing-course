using System;
using System.ComponentModel.DataAnnotations;

namespace TestingTools
{
	public class ProductData
	{
		public ProductData(Guid id, string name, decimal price, string unitsCode)
		{
			Id = id;
			Name = name;
			Price = price;
			UnitsCode = unitsCode;
		}

		public Guid Id { get; set; }

		[MaxLength(30)]
		public string Name { get; set; }
		public decimal Price { get; set; }

		[StringLength(3)]
		public string UnitsCode { get; set; }
	}
}