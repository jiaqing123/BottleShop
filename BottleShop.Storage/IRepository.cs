using System.Collections.Generic;

namespace BottleShop.Storage
{
	public interface IRepository<T> where T : class
	{
		T GetItem(string id, string partitionKey);
		IEnumerable<T> GetItems(string partitionKey, int count = -1);
	}
}
