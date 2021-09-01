using Newtonsoft.Json;

namespace BottleShop.Storage.Entities.TableEntities
{
	public class PromotionColumnEntity
	{
		/// <summary>
		/// Promotion ID
		/// </summary>
		[JsonProperty("id")]
		public string PromotionId { get; set; }

		/// <summary>
		/// Promotion amount
		/// </summary>
		[JsonProperty("a")]
		public decimal? Amount { get; set; }
	}
}
