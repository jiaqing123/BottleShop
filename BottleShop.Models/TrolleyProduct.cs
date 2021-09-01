using Newtonsoft.Json;
using System.Collections.Generic;

namespace BottleShop.Models
{
	public class TrolleyProduct
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("unitPrice")]
		public decimal UnitPrice { get; set; }

		[JsonProperty("quantity")]
		public int Quantity { get; set; }

		[JsonProperty("promotions")]
		public List<Promotion> Promotions { get; set; }

		[JsonProperty("total")]
		public decimal Total { get; set; }

		[JsonProperty("discountedTotal")]
		public decimal DiscountedTotal { get; set; }
	}
}
