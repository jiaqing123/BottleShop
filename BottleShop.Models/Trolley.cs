using Newtonsoft.Json;
using System.Collections.Generic;

namespace BottleShop.Models
{
	public class Trolley
	{
		[JsonProperty("products")]
		public List<TrolleyProduct> Products { get; set; }

		[JsonProperty("promotions")]
		public List<Promotion> Promotions { get; set; }

		[JsonProperty("total")]
		public decimal Total { get; set; }

		[JsonProperty("discountedTotal")]
		public decimal DiscountedTotal { get; set; }
	}
}
