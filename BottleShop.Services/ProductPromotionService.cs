using BottleShop.Storage;
using BottleShop.Storage.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BottleShop.Services
{
	public class ProductPromotionService : IProductPromotionService
	{
		#region IProductPromotionService Members

		public async Task ApplyPromotionsAsync(TrolleyProductEntity trolleyProductEntity)
		{
			if (trolleyProductEntity.Promotions == null || trolleyProductEntity.Promotions.Count <= 0) return;

			var promotionIds = trolleyProductEntity.Promotions.Select(e => e.PromotionId).ToList();

			var promotions = await _promotionRepository.GetPromotionsAsync(promotionIds);

			foreach (var productPromotionEntity in trolleyProductEntity.Promotions)
			{
				var promotion = promotions.FirstOrDefault(e => e.PromotionId == productPromotionEntity.PromotionId);
				if (promotion == null) return;

				if (ApplyAmountOff(promotion, trolleyProductEntity, productPromotionEntity) != null) continue;

				// additional application
			}
		}

		#endregion

		private readonly IPromotionRepository _promotionRepository;

		public ProductPromotionService(IPromotionRepository promotionRepository)
		{
			_promotionRepository = promotionRepository;
		}

		private bool? ApplyAmountOff(PromotionEntity promotion, TrolleyProductEntity trolleyProductEntity, ProductPromotionEntity productPromotionEntity)
		{
			if (promotion.Method != PromotionConstants.METHOD_Unit_OFF) return null;

			var discounted = promotion.ChangeQuantity * -1 * trolleyProductEntity.Quantity ?? 0;

			var total = (trolleyProductEntity.UnitPrice * trolleyProductEntity.Quantity) ?? 0;

			productPromotionEntity.DiscountedAmount = Math.Min(discounted, total);

			return true;
		}
	}
}
