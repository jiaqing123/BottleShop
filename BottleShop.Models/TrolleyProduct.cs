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
		public double UnitPrice { get; set; }

		[JsonProperty("quantity")]
		public int Quantity { get; set; }

		[JsonProperty("promotions")]
		public List<Promotion> Promotions { get; set; }

		[JsonProperty("total")]
		public double Total { get; set; }

		[JsonProperty("discountedTotal")]
		public double DiscountedTotal { get; set; }
	}
}
