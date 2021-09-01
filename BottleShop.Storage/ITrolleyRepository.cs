using BottleShop.Storage.Entities;
using System.Threading.Tasks;

namespace BottleShop.Storage
{
	/// <summary>
	/// Get entity from repository
	/// </summary>
	public interface ITrolleyRepository
	{
		Task<TrolleyEntity> GetAsync(string trolleyId);
		Task<TrolleyEntity> GetAsync(string trolleyId, string productId);
		Task UpdateAsync(TrolleyEntity trolleyEntity);
	}
}
