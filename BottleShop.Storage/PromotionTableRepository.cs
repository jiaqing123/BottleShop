using AutoMapper;
using BottleShop.Storage.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BottleShop.Storage
{
	public class PromotionTableRepository : IPromotionRepository
	{
		#region IPromotionRepository Members

		public async Task<List<PromotionEntity>> GetPromotionsAsync(List<string> promotionIds)
		{
			var keys = promotionIds
				.Select(e => new Tuple<string, string>(e, StorageConstants.ROW_KEY_BASIC))
				.ToList();

			var rows = await _repository.GetRowsAsync(StorageConstants.TABLE_PROMOTION_NAME, keys);

			var entities = _mapper.Map<List<PromotionEntity>>(rows);

			return entities;
		}

		#endregion

		private readonly IMapper _mapper;
		private readonly ITableRepository<PromotionTableEntity> _repository;

		public PromotionTableRepository(IMapper mapper, ITableRepository<PromotionTableEntity> repository)
		{
			_mapper = mapper;
			_repository = repository;
		}
	}
}
