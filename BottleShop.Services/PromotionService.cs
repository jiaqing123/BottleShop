using AutoMapper;
using BottleShop.Storage;
using BottleShop.Storage.Entities;
using System;
using System.Threading.Tasks;

namespace BottleShop.Services
{
	public class PromotionService : IPromotionService
	{
		#region IPromotionService Members

		public Task ApplyPromotionAsync(TrolleyEntity trolleyEntity)
		{
			return Task.CompletedTask;
		}

		public Task ApplyPromotionAsync(TrolleyProductEntity trolleyProductEntity)
		{
			return Task.CompletedTask;
		}

		#endregion

		private readonly IMapper _mapper;
		private readonly IPromotionRepository _promotionRepository;

		public PromotionService(IMapper mapper, IPromotionRepository promotionRepository)
		{
			_mapper = mapper;
			_promotionRepository = promotionRepository;
		}
	}
}
