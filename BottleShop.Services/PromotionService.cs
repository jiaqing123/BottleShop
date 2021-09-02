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

		public async Task ApplyPromotionAsync(TrolleyProductEntity trolleyProductEntity)
		{
			await _productPromotionService.ApplyPromotionsAsync(trolleyProductEntity);
		}

		#endregion

		private readonly IMapper _mapper;
		private readonly IProductPromotionService _productPromotionService;
		private readonly ITrolleyPromotionService _trolleyPromotionService;

		public PromotionService(IMapper mapper, IProductPromotionService productPromotionService, ITrolleyPromotionService trolleyPromotionService)
		{
			_mapper = mapper;
			_productPromotionService = productPromotionService;
			_trolleyPromotionService = trolleyPromotionService;
		}
	}
}
