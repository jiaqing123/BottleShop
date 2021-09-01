using Microsoft.Azure.Cosmos.Table;

namespace BottleShop.Storage.Entities
{
	public class ProductTableEntity : TableEntity
	{
		public string CategoryId { get; set; }

		public string Name { get; set; }

		public double UnitPrice { get; set; }

		public string Promotions { get; set; }
	}
}
