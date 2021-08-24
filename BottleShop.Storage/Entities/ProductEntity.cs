using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BottleShop.Storage.Entities
{
	public class ProductEntity
	{
		[JsonProperty("id")]
		public int Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("price")]
		public decimal Price { get; set; }
	}
}
