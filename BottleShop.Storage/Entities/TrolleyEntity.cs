using Newtonsoft.Json;
using System.Collections.Generic;

namespace BottleShop.Storage.Entities
{
	public class TrolleyEntity
	{
		[JsonProperty("products")]
		public List<ProductEntity> Products { get; set; }
	}
}
