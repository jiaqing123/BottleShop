using Microsoft.Azure.Cosmos.Table;

namespace BottleShop.Storage.Entities
{
	public class PromotionTableEntity : TableEntity
	{
		public double BoundaryQuantity { get; set; }

		public double ChangeQuantity { get; set; }

		public int RepeatCount { get; set; }

		public string Method { get; set; }

		public string Name { get; set; }

		public string Definition { get; set; }

		public string Type { get; set; }
	}
}
