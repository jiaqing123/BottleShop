using Newtonsoft.Json;
using System;

namespace BottleShop.Storage.Entities
{
	public class ProductPromotionEntity
	{
		[JsonProperty("id")]
		public string PromotionId { get; set; }

		[JsonProperty("a")]
		public double? DiscountedAmount { get; set; }
	}
}
