using BottleShop.Storage.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BottleShop.Storage
{
	public interface IProductRepository
	{
		Task<ProductEntity> GetProductAsync(string productId);
		Task<List<ProductEntity>> GetProductsAsync(List<string> productIds);
	}
}
