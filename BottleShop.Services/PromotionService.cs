using AutoMapper;
using BottleShop.Storage.Entities;
using System.Threading.Tasks;

namespace BottleShop.Services
{
	public class PromotionService : IPromotionService
	{
		#region IPromotionService Members

		public async Task ApplyPromotionAsync(TrolleyEntity trolleyEntity)
		{
			await _trolleyPromotionService.ApplyPromotionsAsync(trolleyEntity);
		}

		public Task ApplyPromotionAsync(TrolleyProductEntity trolleyProductEntity)
		{
			return Task.CompletedTask;
		}

		#endregion

		private readonly IMapper _mapper;
		private readonly ITrolleyPromotionService _trolleyPromotionService;

		public PromotionService(IMapper mapper, ITrolleyPromotionService trolleyPromotionService)
		{
			_mapper = mapper;
			_trolleyPromotionService = trolleyPromotionService;
		}
	}
}
