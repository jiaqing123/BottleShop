using System.Collections.Generic;

namespace BottleShop.Storage.Entities
{
	public class TrolleyProductEntity
	{
		public string TrolleyId { get; set; }

		public string ProductId { get; set; }

		public decimal UnitPrice { get; set; }

		public decimal Quantity { get; set; }

		public List<ProductPromotionEntity> Promotions { get; set; }

		public decimal Total { get; set; }

		public decimal DiscountedTotal { get; set; }

		public string ETag { get; set; }

		public TrolleyProductEntity()
		{
			Promotions = new List<ProductPromotionEntity>();
		}
	}
}
