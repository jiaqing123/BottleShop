using BottleShop.Storage.Entities;
using System.Threading.Tasks;

namespace BottleShop.Services
{
	public interface IPromotionService
	{
		/// <summary>
		/// Apply and update promotions
		/// </summary>
		/// <param name="trolleyEntity"></param>
		/// <returns></returns>
		Task ApplyPromotionAsync(TrolleyEntity trolleyEntity);

		/// <summary>
		/// Apply and update promotions
		/// </summary>
		/// <param name="trolleyProductEntity"></param>
		/// <returns></returns>
		Task ApplyPromotionAsync(TrolleyProductEntity trolleyProductEntity);
	}
}
