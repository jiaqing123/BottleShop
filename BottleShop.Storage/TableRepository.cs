using Microsoft.Azure.Cosmos.Table;
using Microsoft.Azure.Cosmos.Table.Queryable;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BottleShop.Storage
{
	public class TableRepository<T> : ITableRepository<T> where T : ITableEntity, new()
	{
		#region IDocumentRepository Members

		public async Task<Tuple<List<U>, string>> GetRowsAync<U>(string tableName, Expression<Func<T, bool>> predicate, Expression<Func<T, U>> selector, int count = -1, string continuationToken = null) where U : ITableEntity, new()
		{
			var account = GetAccount();
			var rows = new List<U>();

			var pageSize = StorageConstants.QUERY_PAGE_SIZE;
			if (count > 0 && count < pageSize)
			{
				pageSize = count;
			}

			var token = ToToken(continuationToken);

			var client = account.CreateCloudTableClient();

			var table = client.GetTableReference(tableName);

			var query = table.CreateQuery<T>()
				.Where(predicate)
				.Select(selector)
				.Take(pageSize)
				.AsTableQuery();

			var result = await query.ExecuteSegmentedAsync(token);

			if (result != null)
			{
				rows.AddRange(result);
			}

			return new Tuple<List<U>, string>(
				rows,
				ToTokenString(result?.ContinuationToken)
				);
		}

		public async Task<List<T>> GetRowsAsync(string tableName, List<Tuple<string, string>> keys, List<string> columns = null)
		{
			var account = GetAccount();
			var rows = new List<T>();

			var client = account.CreateCloudTableClient();

			var table = client.GetTableReference(tableName);

			var partitionBatchGroups = keys
				.GroupBy(e => e.Item1);

			foreach (var partitionBatchGrp in partitionBatchGroups)
			{
				var operation = new TableBatchOperation();

				foreach (var key in partitionBatchGrp)
				{
					operation.Retrieve<T>(key.Item1, key.Item2, columns);

					if (operation.Count >= StorageConstants.BATCH_SIZE)
					{
						var results = await table.ExecuteBatchAsync(operation);

						rows.AddRange(results.Select(e => e.Result).Cast<T>());

						operation = new TableBatchOperation();
					}
				}

				if (operation.Count > 0)
				{
					var results = await table.ExecuteBatchAsync(operation);

					rows.AddRange(results.Select(e => e.Result).Cast<T>());
				}
			}

			return rows;
		}

		public async Task<T> GetRowAsync(string tableName, string partitionKey, string rowKey)
		{
			var account = GetAccount();

			var client = account.CreateCloudTableClient();

			var table = client.GetTableReference(tableName);

			var operation = TableOperation.Retrieve<T>(partitionKey, rowKey);

			var result = await table.ExecuteAsync(operation);

			if (result?.Result == null)
			{
				return default(T);
			}
			else
			{
				return (T)result.Result;
			}
		}

		public async Task<List<T>> UpdateAsync(string tableName, IEnumerable<T> rows)
		{
			var account = GetAccount();
			var batchSize = StorageConstants.BATCH_SIZE;
			var resultRows = new List<T>();

			var client = account.CreateCloudTableClient();

			var table = client.GetTableReference(tableName);

			// group by partitionKey and divide in batch
			var partitionBatchGroups = rows
				.Select(e => e)
				.GroupBy(e => e.PartitionKey)
				.Select((grp) => new
				{
					Batches = grp
					.Select((e, i) => new { Index = i, Value = e })
					.GroupBy(a => a.Index / batchSize),
				});

			foreach (var partitionBatchGrp in partitionBatchGroups)
			{
				foreach (var entitiyBatches in partitionBatchGrp.Batches)
				{
					var operation = new TableBatchOperation();

					foreach (var entity in entitiyBatches)
					{
						if (string.IsNullOrEmpty(entity.Value.ETag))
						{
							operation.Insert(entity.Value);
						}
						else if (entity.Value.ETag == "*")
						{
							operation.InsertOrReplace(entity.Value);
						}
						else
						{
							operation.InsertOrMerge(entity.Value);
						}
					}

					var result = await table.ExecuteBatchAsync(operation);

					resultRows.AddRange(result.Select(e => (T)e.Result));
				}
			}

			return resultRows;
		}

		public async Task UpdateAsync(string tableName, T row)
		{
			var account = GetAccount();

			var client = account.CreateCloudTableClient();

			var table = client.GetTableReference(tableName);

			var operation = default(TableOperation);

			if (row.ETag == "*")
			{
				operation = TableOperation.InsertOrReplace(row);
			}
			else
			{
				operation = TableOperation.InsertOrMerge(row);
			}

			await table.ExecuteAsync(operation);
		}

		#endregion

		private readonly StorageConfiguration _configuration;

		public TableRepository(StorageConfiguration configuration)
		{
			_configuration = configuration;
		}

		private CloudStorageAccount GetAccount()
		{
			return CloudStorageAccount.Parse(_configuration.ConnectionString);
		}

		private string ToTokenString(TableContinuationToken token)
		{
			if (token == null) return null;

			return JsonConvert.SerializeObject(token);
		}

		private TableContinuationToken ToToken(string tokenString)
		{
			if (string.IsNullOrEmpty(tokenString)) return null;

			return JsonConvert.DeserializeObject<TableContinuationToken>(tokenString);
		}
	}
}
