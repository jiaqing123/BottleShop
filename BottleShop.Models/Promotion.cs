using Newtonsoft.Json;

namespace BottleShop.Models
{
	public class Promotion
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("definition")]
		public string Definition { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }

		[JsonProperty("discountedAmount")]
		public double? DiscountedAmount { get; set; }
	}
}
