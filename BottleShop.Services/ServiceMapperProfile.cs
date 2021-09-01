using AutoMapper;
using BottleShop.Models;
using BottleShop.Storage.Entities;

namespace BottleShop.Services
{
	public class ServiceMapperProfile : Profile
	{
		public ServiceMapperProfile()
		{
			#region TrolleyEntity - Trolley

			CreateMap<TrolleyEntity, Trolley>()
				.ForMember(dest => dest.DiscountedTotal, opts => opts.MapFrom(src => src.DiscountedTotal))
				.ForMember(dest => dest.Products, opts => opts.MapFrom(src => src.Products))
				.ForMember(dest => dest.Promotions, opts => opts.MapFrom(src => src.Promotions))
				.ForMember(dest => dest.Total, opts => opts.MapFrom(src => src.Total))
				.ForAllMembers(a => a.Condition((source, target, sourceValue, targetValue) => sourceValue != null));

			#endregion

			#region TrolleyProductEntity - TrolleyProduct

			CreateMap<TrolleyProductEntity, TrolleyProduct>()
				.ForMember(dest => dest.DiscountedTotal, opts => opts.MapFrom(src => src.DiscountedTotal))
				.ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.ProductId))
				.ForMember(dest => dest.Name, opts => opts.Ignore())
				.ForMember(dest => dest.Promotions, opts => opts.MapFrom(src => src.Promotions))
				.ForMember(dest => dest.Quantity, opts => opts.MapFrom(src => src.Quantity))
				.ForMember(dest => dest.Total, opts => opts.MapFrom(src => src.Total))
				.ForMember(dest => dest.UnitPrice, opts => opts.MapFrom(src => src.UnitPrice))
				.ForAllMembers(a => a.Condition((source, target, sourceValue, targetValue) => sourceValue != null));

			#endregion

			#region ProductPromotionEntity - Promotion

			CreateMap<ProductPromotionEntity, Promotion>()
				.ForMember(dest => dest.Definition, opts => opts.Ignore())
				.ForMember(dest => dest.DiscountedAmount, opts => opts.MapFrom(src => src.DiscountedAmount))
				.ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.PromotionId))
				.ForMember(dest => dest.Name, opts => opts.Ignore())
				.ForMember(dest => dest.Type, opts => opts.Ignore())
				.ForAllMembers(a => a.Condition((source, target, sourceValue, targetValue) => sourceValue != null));

			#endregion

			#region TrolleyPromotionEntity - Promotion

			CreateMap<TrolleyPromotionEntity, Promotion>()
				.ForMember(dest => dest.Definition, opts => opts.Ignore())
				.ForMember(dest => dest.DiscountedAmount, opts => opts.MapFrom(src => src.DiscountedAmount))
				.ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.PromotionId))
				.ForMember(dest => dest.Name, opts => opts.Ignore())
				.ForMember(dest => dest.Type, opts => opts.Ignore())
				.ForAllMembers(a => a.Condition((source, target, sourceValue, targetValue) => sourceValue != null));

			#endregion

			#region PromotionEntity - Promotion

			CreateMap<PromotionEntity, Promotion>()
				.ForMember(dest => dest.Definition, opts => opts.MapFrom(src => src.Definition))
				.ForMember(dest => dest.DiscountedAmount, opts => opts.Ignore())				
				.ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.PromotionId))
				.ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
				.ForMember(dest => dest.Type, opts => opts.MapFrom(src => src.Type))
				.ForAllMembers(a => a.Condition((source, target, sourceValue, targetValue) => sourceValue != null));

			#endregion

			#region ProductEntity - TrolleyProductEntity

			CreateMap<ProductEntity, TrolleyProductEntity>()
				.ForMember(dest => dest.DiscountedTotal, opts => opts.Ignore())
				.ForMember(dest => dest.ETag, opts => opts.Ignore())
				.ForMember(dest => dest.ProductId, opts => opts.MapFrom(src => src.ProductId))
				.ForMember(dest => dest.Promotions, opts => opts.MapFrom(src => src.Promotions))
				.ForMember(dest => dest.Quantity, opts => opts.Ignore())
				.ForMember(dest => dest.Total, opts => opts.Ignore())
				.ForMember(dest => dest.TrolleyId, opts => opts.Ignore())
				.ForMember(dest => dest.UnitPrice, opts => opts.MapFrom(src => src.UnitPrice))
				.ForAllMembers(a => a.Condition((source, target, sourceValue, targetValue) => sourceValue != null));

			#endregion

			#region PromotionEntity - ProductPromotionEntity

			CreateMap<PromotionEntity, ProductPromotionEntity>()
				.ForMember(dest => dest.DiscountedAmount, opts => opts.Ignore())
				.ForMember(dest => dest.PromotionId, opts => opts.MapFrom(src => src.PromotionId))
				.ForAllMembers(a => a.Condition((source, target, sourceValue, targetValue) => sourceValue != null));

			#endregion
		}
	}
}
