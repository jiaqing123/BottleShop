using System.Collections.Generic;

namespace BottleShop.Storage.Entities
{
	public class TrolleyEntity
	{
		public string TrolleyId { get; set; }

		public List<TrolleyProductEntity> Products { get; set; }

		public List<TrolleyPromotionEntity> Promotions { get; set; }

		public double Total { get; set; }

		public double DiscountedTotal { get; set; }

		public string ETag { get; set; }

		public TrolleyEntity()
		{
			Products = new List<TrolleyProductEntity>();
			Promotions = new List<TrolleyPromotionEntity>();
		}
	}
}
