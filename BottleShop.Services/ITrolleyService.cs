using BottleShop.Models;
using System.Threading.Tasks;

namespace BottleShop.Services
{
	public interface ITrolleyService
	{
		/// <summary>
		/// Decrease the quantity of a product in a trolley
		/// </summary>
		/// <param name="customerId"></param>
		/// <param name="trolleyId"></param>
		/// <param name="productId"></param>
		/// <param name="quantity"></param>
		/// <returns>Part of the trolley. Contains trolley basic info and affected product info. Null if product is not found in trolley.</returns>
		Task<Trolley> DecreaseProductQuantityAsync(string customerId, string trolleyId, string productId, int quantity);

		/// <summary>
		/// Get a trolley from a customer
		/// </summary>
		/// <param name="customerId"></param>
		/// <param name="trolleyId"></param>
		/// <returns>Full trolley info. Null if trolley not found</returns>
		Task<Trolley> GetTrolleyAsync(string trolleyId);

		/// <summary>
		/// Increase the quantity of a product in a trolley
		/// </summary>
		/// <param name="customerId"></param>
		/// <param name="trolleyId"></param>
		/// <param name="productId"></param>
		/// <param name="quantity"></param>
		/// <returns>Part of the trolley. Contains trolley basic info and affected product info. Null if product is not found in product list</returns>
		/// <remarks>
		/// If product is not found in trolley, the product will be added.
		/// </remarks>
		Task<Trolley> IncreaseProductQuantityAsync(string customerId, string trolleyId, string productId, int quantity);
	}
}
