using Newtonsoft.Json;

namespace BottleShop.Storage.Entities
{
	public class PromotionEntity
	{
		public double BoundaryQuantity { get; set; }

		public double ChangeQuantity { get; set; }

		public int RepeatCount { get; set; }

		public string Definition { get; set; }

		public string ETag { get; set; }

		public string Method { get; set; }

		public string Name { get; set; }

		[JsonProperty("id")]
		public string PromotionId { get; set; }

		public string Type { get; set; }

		

	}
}
