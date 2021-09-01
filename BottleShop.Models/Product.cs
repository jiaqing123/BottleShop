using Newtonsoft.Json;
using System.Collections.Generic;

namespace BottleShop.Models
{
	public class Product
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("unitPrice")]
		public double UnitPrice { get; set; }

		[JsonProperty("promotions")]
		public List<Promotion> Promotions { get; set; }
	}
}
