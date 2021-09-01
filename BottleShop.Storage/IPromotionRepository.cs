using BottleShop.Storage.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BottleShop.Storage
{
	public interface IPromotionRepository
	{
		Task<List<PromotionEntity>> GetPromotionsAsync(List<string> promotionIds);
	}
}
