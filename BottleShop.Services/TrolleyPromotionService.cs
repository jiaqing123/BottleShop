using BottleShop.Storage;
using BottleShop.Storage.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BottleShop.Services
{
	public class TrolleyPromotionService : ITrolleyPromotionService
	{
		#region ITrolleyPromotionService Members

		public async Task ApplyPromotionsAsync(TrolleyEntity trolleyEntity)
		{
			if (trolleyEntity.Promotions == null || trolleyEntity.Promotions.Count <= 0) return;

			var promotionIds = trolleyEntity.Promotions.Select(e => e.PromotionId).ToList();

			var promotions = await _promotionRepository.GetPromotionsAsync(promotionIds);

			foreach (var trolleyPromotionEntity in trolleyEntity.Promotions)
			{
				var promotion = promotions.FirstOrDefault(e => e.PromotionId == trolleyPromotionEntity.PromotionId);
				if (promotion == null) return;

				if (ApplySpendMoreAndSave(promotion, trolleyEntity, trolleyPromotionEntity)) continue;

				// additional application
			}
		}

		#endregion

		private readonly IPromotionRepository _promotionRepository;

		public TrolleyPromotionService(IPromotionRepository promotionRepository)
		{
			_promotionRepository = promotionRepository;
		}

		private bool ApplySpendMoreAndSave(PromotionEntity promotion, TrolleyEntity trolleyEntity, TrolleyPromotionEntity trolleyPromotionEntity)
		{
			if (promotion.Method != PromotionConstants.METHOD_SPEND_MORE_AND_SAVE) return false;

			var count = Math.Floor(trolleyEntity.Total / promotion.BoundaryQuantity);

			if (count > 0)
			{
				if (promotion.RepeatCount > 0)
				{
					count = Math.Min(count, promotion.RepeatCount);
				}

				trolleyPromotionEntity.DiscountedAmount = promotion.ChangeQuantity * -1 * count;

				return true;
			}
			else
			{
				trolleyPromotionEntity.DiscountedAmount = 0;

				return false;
			}
		}
	}
}
