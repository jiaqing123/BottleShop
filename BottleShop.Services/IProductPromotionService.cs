using BottleShop.Storage.Entities;
using System.Threading.Tasks;

namespace BottleShop.Services
{
	public interface IProductPromotionService
	{
		Task ApplyPromotionsAsync(TrolleyProductEntity trolleyProductEntity);
	}
}
