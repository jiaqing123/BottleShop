using AutoMapper;
using BottleShop.Storage.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BottleShop.Storage
{
	public class ProductTableRepository : IProductRepository
	{
		#region IProductRepository Members

		public async Task<ProductEntity> GetProductAsync(string productId)
		{
			var tableName = StorageConstants.TABLE_PRODUCT_NAME;

			var row = await _repository.GetRowAsync(tableName, productId, StorageConstants.ROW_KEY_BASIC);

			var entity = _mapper.Map<ProductEntity>(row);

			return entity;
		}

		public async Task<List<ProductEntity>> GetProductsAsync(List<string> productIds)
		{
			var tableName = StorageConstants.TABLE_PRODUCT_NAME;

			var keys = productIds
				.Select(e => new Tuple<string, string>(e, StorageConstants.ROW_KEY_BASIC))
				.ToList();

			var rows = await _repository.GetRowsAsync(tableName, keys);

			var entities = _mapper.Map<List<ProductEntity>>(rows);

			return entities;
		}

		#endregion

		private readonly IMapper _mapper;
		private readonly ITableRepository<ProductTableEntity> _repository;

		public ProductTableRepository(IMapper mapper, ITableRepository<ProductTableEntity> repository)
		{
			_mapper = mapper;
			_repository = repository;
		}
	}
}
