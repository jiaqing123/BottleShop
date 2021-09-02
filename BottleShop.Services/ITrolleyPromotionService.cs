using BottleShop.Storage.Entities;
using System.Threading.Tasks;

namespace BottleShop.Services
{
	public interface ITrolleyPromotionService
	{
		Task ApplyPromotionsAsync(TrolleyEntity trolleyEntity);
	}
}
