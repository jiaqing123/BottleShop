using Newtonsoft.Json;

namespace BottleShop.Models
{
	public class Product
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("price")]
		public decimal Price { get; set; }
	}
}
