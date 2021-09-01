using Newtonsoft.Json;
using System.Collections.Generic;

namespace BottleShop.Storage.Entities
{
	public class TrolleyProductEntity
	{
		[JsonProperty("trolleyId")]
		public string TrolleyId { get; set; }

		[JsonProperty("id")]
		public string ProductId { get; set; }

		[JsonProperty("unitPrice")]
		public double? UnitPrice { get; set; }

		[JsonProperty("quantity")]
		public double? Quantity { get; set; }

		[JsonProperty("promotions")]
		public List<ProductPromotionEntity> Promotions { get; set; }

		[JsonProperty("total")]
		public double? Total { get; set; }

		[JsonProperty("discountedTotal")]
		public double? DiscountedTotal { get; set; }

		[JsonProperty("eTag")]
		public string ETag { get; set; }
	}
}
