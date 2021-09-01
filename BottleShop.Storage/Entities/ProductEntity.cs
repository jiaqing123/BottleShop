using System.Collections.Generic;

namespace BottleShop.Storage.Entities
{
	public class ProductEntity
	{
		public decimal UnitPrice { get; set; }

		public List<PromotionEntity> Promotions { get; set; }

		public string CategoryId { get; set; }

		public string ETag { get; set; }

		public string Name { get; set; }

		public string ProductId { get; set; }
	}
}
