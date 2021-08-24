using Newtonsoft.Json;
using System.Collections.Generic;

namespace BottleShop.Models
{
	public class Trolley
	{
		[JsonProperty("products")]
		public List<Product> Products { get; set; }
	}
}
