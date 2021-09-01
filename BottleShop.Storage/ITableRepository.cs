using Microsoft.Azure.Cosmos.Table;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BottleShop.Storage
{
	/// <summary>
	/// Get table entity from storage
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public interface ITableRepository<T> where T : ITableEntity
	{
		Task<Tuple<List<U>, string>> GetRowsAync<U>(string tableName, Expression<Func<T, bool>> predicate, Expression<Func<T, U>> selector, int count = -1, string continuationToken = null) where U : ITableEntity, new();
		
		Task<T> GetRowAsync(string tableName, string partitionKey, string rowKey);

		Task<List<T>> GetRowsAsync(string tableName, List<Tuple<string, string>> keys, List<string> columns = null);

		Task<List<T>> UpdateAsync(string tableName, IEnumerable<T> rows);
		
		Task UpdateAsync(string tableName, T row);
	}
}
