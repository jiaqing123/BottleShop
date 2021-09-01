using Microsoft.Azure.Cosmos.Table;

namespace BottleShop.Storage.Entities
{
	public class PromotionTableEntity : TableEntity
	{
		public string Name { get; set; }

		public string Definition { get; set; }

		public string Type { get; set; }
	}
}
