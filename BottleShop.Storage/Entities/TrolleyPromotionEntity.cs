using Newtonsoft.Json;

namespace BottleShop.Storage.Entities
{
	public class TrolleyPromotionEntity
	{
		[JsonProperty("id")]
		public string PromotionId { get; set; }

		[JsonProperty("a")]
		public double DiscountedAmount { get; set; }
	}
}
