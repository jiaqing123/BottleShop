using AutoMapper;
using BottleShop.Storage.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BottleShop.Storage
{
	public class TrolleyTableRepository : ITrolleyRepository
	{
		#region ITrolleyRepository Members

		public async Task<TrolleyEntity> GetAsync(string trolleyId)
		{
			var rows = await GetRowsAsync(trolleyId);
			if (rows == null) return null;

			var entity = MapToEntity(rows);

			return entity;
		}

		public async Task<TrolleyEntity> GetAsync(string trolleyId, string productId)
		{
			var rows = await GetRowsAsync(trolleyId, productId);

			var entity = MapToEntity(rows);

			return entity;
		}

		public async Task UpdateAsync(TrolleyEntity trolleyEntity)
		{
			var rows = MapToRows(trolleyEntity);

			await UpdateRowsAsync(rows);

			MapToEntity(rows, trolleyEntity);
		}

		#endregion

		private readonly IMapper _mapper;
		private readonly ITableRepository<TrolleyTableEntity> _repository;

		public TrolleyTableRepository(IMapper mapper, ITableRepository<TrolleyTableEntity> repository)
		{
			_mapper = mapper;
			_repository = repository;
		}

		private async Task<List<TrolleyTableEntity>> GetRowsAsync(string trolleyId)
		{
			var rows = new List<TrolleyTableEntity>();
			var rowsResult = default(Tuple<List<TrolleyTableEntity>, string>);
			var tableName = StorageConstants.TABLE_TROLLEY_NAME;

			do
			{
				rowsResult = await _repository.GetRowsAync(
					tableName,
					e => e.PartitionKey == trolleyId,
					e => e);

				rows.AddRange(rowsResult.Item1);

			} while (rowsResult?.Item2 != null);

			return rows;
		}

		private async Task<List<TrolleyTableEntity>> GetRowsAsync(string trolleyId, string productId)
		{
			var rows = new List<TrolleyTableEntity>();
			var tableName = StorageConstants.TABLE_TROLLEY_NAME;

			var basicRow = await _repository.GetRowAsync(tableName, trolleyId, StorageConstants.ROW_KEY_BASIC);
			if (basicRow == null) return null;

			rows.Add(basicRow);

			var productRowKey = StorageUtil.GetRowKeyContent(new Tuple<string, string>(StorageConstants.ROW_ID_PRODUCT, productId));

			var productRow = await _repository.GetRowAsync(tableName, trolleyId, productRowKey);

			if (productRow != null)
			{
				rows.Add(productRow);
			}

			return rows;
		}

		private TrolleyEntity MapToEntity(IEnumerable<TrolleyTableEntity> rows, TrolleyEntity entity)
		{
			foreach (var row in rows)
			{
				if (row.RowType == StorageConstants.ROW_TYPE_BASIC)
				{
					// trolley basic info
					_mapper.Map(row, entity);
				}
				else if (row.RowType == StorageConstants.ROW_TYPE_PRODUCT)
				{
					if (entity.Products == null) entity.Products = new List<TrolleyProductEntity>();

					// product
					var product = _mapper.Map<TrolleyProductEntity>(row);

					var existing = entity.Products.FirstOrDefault(e => e.ProductId == product.ProductId);

					if (existing == null)
					{
						entity.Products.Add(product);
					}
					else
					{
						_mapper.Map(row, existing);
					}
				}
			}

			return entity;
		}

		private TrolleyEntity MapToEntity(IEnumerable<TrolleyTableEntity> rows)
		{
			return MapToEntity(rows, new TrolleyEntity());
		}

		private IEnumerable<TrolleyTableEntity> MapToRows(TrolleyEntity trolleyEntity)
		{
			var basicRow = _mapper.Map<TrolleyTableEntity>(trolleyEntity);
			yield return basicRow;

			if (trolleyEntity.Products?.Count > 0)
			{
				foreach (var trolleyProduct in trolleyEntity.Products)
				{
					// only get affected products
					if (string.IsNullOrEmpty(trolleyProduct.ETag)) continue;

					var productRow = _mapper.Map<TrolleyTableEntity>(trolleyProduct);

					yield return productRow;
				}
			}
		}

		private async Task UpdateRowsAsync(IEnumerable<TrolleyTableEntity> rows)
		{
			var tableName = StorageConstants.TABLE_TROLLEY_NAME;

			await _repository.UpdateAsync(tableName, rows);
		}
	}
}
