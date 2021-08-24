using BottleShop.Models;
using System.Threading.Tasks;

namespace BottleShop.Services
{
	public interface ITrolleyService
	{
		Task<Trolley> GetCustomerTrolleyAsync(string customerId);
	}
}
