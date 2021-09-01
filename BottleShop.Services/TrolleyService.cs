using AutoMapper;
using BottleShop.Models;
using BottleShop.Storage;
using BottleShop.Storage.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BottleShop.Services
{
	public class TrolleyService : ITrolleyService
	{
		#region ITrolleyService Members

		public async Task<Trolley> DecreaseProductQuantityAsync(string customerId, string trolleyId, string productId, int quantity)
		{
			if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));

			var trolleyEntity = await _trolleyRepository.GetAsync(trolleyId, productId);
			if (trolleyEntity == null) return null;

			var trolleyProductEntity = trolleyEntity.Products.FirstOrDefault(e => e.ProductId == productId);
			if (trolleyProductEntity == null) return null;

			trolleyProductEntity.Quantity -= quantity;
			if (trolleyProductEntity.Quantity < 0) trolleyProductEntity.Quantity = 0;

			var productTotals = await CalculateTotalsAsync(trolleyProductEntity);

			await UpdateProductAsync(trolleyEntity, trolleyProductEntity, productTotals);

			await _trolleyRepository.UpdateAsync(trolleyEntity);

			var trolley = _mapper.Map<Trolley>(trolleyEntity);

			return trolley;
		}

		public async Task<Trolley> GetTrolleyAsync(string trolleyId)
		{
			var trolleyEntity = await _trolleyRepository.GetAsync(trolleyId);
			if (trolleyEntity == null) return null;

			var trolley = _mapper.Map<Trolley>(trolleyEntity);

			await LoadProductInfoAsync(trolley);

			await LoadPromotionInfoAsync(trolley);

			return trolley;
		}

		public async Task<Trolley> IncreaseProductQuantityAsync(string customerId, string trolleyId, string productId, int quantity)
		{
			if (quantity <= 0) throw new ArgumentOutOfRangeException(nameof(quantity));

			// get product from trolley
			var trolleyEntity = await _trolleyRepository.GetAsync(trolleyId, productId);
			if (trolleyEntity == null) return null;

			var trolleyProductEntity = trolleyEntity.Products.FirstOrDefault(e => e.ProductId == productId);

			if (trolleyProductEntity == null)
			{
				// add new trolley product
				var productEntity = await _productRepository.GetProductAsync(productId);
				if (productEntity == null) return null;

				trolleyProductEntity = _mapper.Map<TrolleyProductEntity>(productEntity);

				trolleyProductEntity.Quantity = quantity;
				trolleyProductEntity.TrolleyId = trolleyId;

				var productTotals = await CalculateTotalsAsync(trolleyProductEntity);

				await AddProductAsync(trolleyEntity, trolleyProductEntity, productTotals);
			}
			else
			{
				// update existing trolley product
				trolleyProductEntity.Quantity += quantity;

				var productTotals = await CalculateTotalsAsync(trolleyProductEntity);

				await UpdateProductAsync(trolleyEntity, trolleyProductEntity, productTotals);
			}

			await _trolleyRepository.UpdateAsync(trolleyEntity);

			var trolley = _mapper.Map<Trolley>(trolleyEntity);

			return trolley;
		}

		#endregion

		private readonly IMapper _mapper;
		private readonly IProductRepository _productRepository;
		private readonly IPromotionRepository _promotionRepository;
		private readonly ITrolleyRepository _trolleyRepository;
		private readonly IPromotionService _promotionService;

		public TrolleyService(
			IMapper mapper,
			IProductRepository productRepository,
			IPromotionRepository promotionRepository,
			IPromotionService promotionService,
			ITrolleyRepository trolleyRepository			
			)
		{
			_mapper = mapper;
			_productRepository = productRepository;
			_promotionRepository = promotionRepository;
			_promotionService = promotionService;
			_trolleyRepository = trolleyRepository;

			_mapper.ConfigurationProvider.AssertConfigurationIsValid();
		}

		private async Task AddProductAsync(TrolleyEntity trolleyEntity, TrolleyProductEntity trolleyProductEntity, Totals productTotals)
		{
			trolleyProductEntity.Total = productTotals.Total;
			trolleyProductEntity.DiscountedTotal = productTotals.DiscountedTotal;
			trolleyProductEntity.ETag = "*";

			trolleyEntity.Products.Add(trolleyProductEntity);

			trolleyEntity.Total += trolleyProductEntity.DiscountedTotal;

			await _promotionService.ApplyPromotionAsync(trolleyEntity);

			var discount = trolleyEntity.Promotions?.Sum(e => e.DiscountedAmount) ?? 0;

			trolleyEntity.DiscountedTotal = trolleyEntity.Total - discount;
		}

		/// <summary>
		/// Update promotion amount and calculate totals
		/// </summary>
		/// <param name="trolleyProductEntity"></param>
		/// <returns></returns>
		private async Task<Totals> CalculateTotalsAsync(TrolleyProductEntity trolleyProductEntity)
		{
			var totals = new Totals();

			if (trolleyProductEntity.Quantity > 0)
			{
				totals.Total = trolleyProductEntity.UnitPrice * trolleyProductEntity.Quantity;

				await _promotionService.ApplyPromotionAsync(trolleyProductEntity);

				var discount = trolleyProductEntity.Promotions?.Sum(e => e.DiscountedAmount) ?? 0;

				totals.DiscountedTotal = totals.Total - discount;
			}
			else
			{
				totals.Total = 0;
				totals.DiscountedTotal = 0;
			}

			return totals;
		}

		private IEnumerable<TrolleyProduct> GetProducts(Trolley trolley)
		{
			if (trolley.Products?.Count > 0)
			{
				return trolley.Products;
			}
			else
			{
				return Enumerable.Empty<TrolleyProduct>();
			}
		}

		private IEnumerable<Promotion> GetPromotions(Trolley trolley)
		{
			if (trolley.Products?.Count > 0)
			{
				foreach (var product in trolley.Products)
				{
					if (product.Promotions?.Count > 0)
					{
						foreach (var promotion in product.Promotions)
						{
							yield return promotion;
						}
					}
				}
			}

			if (trolley.Promotions?.Count > 0)
			{
				foreach (var promotion in trolley.Promotions)
				{
					yield return promotion;
				}
			}
		}

		private async Task LoadProductInfoAsync(Trolley trolley)
		{
			var products = GetProducts(trolley);

			var productIds = products.Select(e => e.Id).Distinct().ToList();

			var productEntities = await _productRepository.GetProductsAsync(productIds);

			foreach (var product in products)
			{
				var entity = productEntities.FirstOrDefault(e => e.ProductId == product.Id);
				if (entity == null) continue;

				product.Name = entity.Name;				
			}
		}

		private async Task LoadPromotionInfoAsync(Trolley trolley)
		{
			var promotions = GetPromotions(trolley);

			var promotionIds = promotions.Select(e => e.Id).Distinct().ToList();

			var promotionEntities = await _promotionRepository.GetPromotionsAsync(promotionIds);

			foreach (var promotion in promotions)
			{
				var entity = promotionEntities.FirstOrDefault(e => e.PromotionId == promotion.Id);
				if (entity == null) continue;

				promotion.Definition = entity.Definition;
				promotion.Name = entity.Name;
				promotion.Type = entity.Type;
			}
		}

		private async Task UpdateProductAsync(TrolleyEntity trolleyEntity, TrolleyProductEntity trolleyProductEntity, Totals productTotals)
		{
			trolleyEntity.Total -= trolleyProductEntity.DiscountedTotal;

			trolleyProductEntity.Total = productTotals.Total;
			trolleyProductEntity.DiscountedTotal = productTotals.DiscountedTotal;

			trolleyEntity.Total += trolleyProductEntity.DiscountedTotal;

			await _promotionService.ApplyPromotionAsync(trolleyEntity);

			var discount = trolleyEntity.Promotions?.Sum(e => e.DiscountedAmount) ?? 0;

			trolleyEntity.DiscountedTotal = trolleyEntity.Total - discount;
		}

		class Totals
		{
			public decimal DiscountedTotal { get; set; }
			public decimal Total { get; set; }
		}
	}
}
