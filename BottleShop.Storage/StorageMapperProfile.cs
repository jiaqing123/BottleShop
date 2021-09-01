using AutoMapper;
using BottleShop.Storage.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BottleShop.Storage
{
	public class StorageMapperProfile : Profile
	{
		public StorageMapperProfile()
		{
			#region TrolleyTableEntity - TrolleyEntity

			CreateMap<TrolleyTableEntity, TrolleyEntity>()
				.ForMember(dest => dest.DiscountedTotal, opts => opts.MapFrom(src => src.DiscountedTotal ?? 0))
				.ForMember(dest => dest.ETag, opts => opts.MapFrom(src => src.ETag))
				.ForMember(dest => dest.Products, opts => opts.MapFrom(src => GetTrolleyProductList(src)))
				.ForMember(dest => dest.Promotions, opts => opts.MapFrom(src => StorageUtil.GetContentList<TrolleyPromotionEntity>(src.Promotions)))
				.ForMember(dest => dest.Total, opts => opts.MapFrom(src => src.Total ?? 0))
				.ForMember(dest => dest.TrolleyId, opts => opts.MapFrom(src => src.PartitionKey))
				.ForAllMembers(a => a.Condition((source, target, sourceValue, targetValue) => sourceValue != null));


			CreateMap<TrolleyEntity, TrolleyTableEntity>()
				.ForMember(dest => dest.Children, opts => opts.MapFrom(src => GetTrolleyChildrenString(src)))
				.ForMember(dest => dest.DiscountedTotal, opts => opts.MapFrom(src => src.DiscountedTotal))
				.ForMember(dest => dest.ETag, opts => opts.MapFrom(src => src.ETag))
				.ForMember(dest => dest.PartitionKey, opts => opts.MapFrom(src => src.TrolleyId))
				.ForMember(dest => dest.Promotions, opts => opts.MapFrom(src => StorageUtil.GetContentString(src.Promotions)))
				.ForMember(dest => dest.Quantity, opts => opts.Ignore())
				.ForMember(dest => dest.RowKey, opts => opts.MapFrom(src => StorageConstants.ROW_KEY_BASIC))
				.ForMember(dest => dest.RowType, opts => opts.MapFrom(src => StorageConstants.ROW_TYPE_BASIC))
				.ForMember(dest => dest.Timestamp, opts => opts.Ignore())
				.ForMember(dest => dest.Total, opts => opts.MapFrom(src => src.Total))
				.ForMember(dest => dest.UnitPrice, opts => opts.Ignore())
				.ForAllMembers(a => a.Condition((source, target, sourceValue, targetValue) => sourceValue != null));

			#endregion

			#region TrolleyTableEntity - TrolleyProductEntity

			CreateMap<TrolleyTableEntity, TrolleyProductEntity>()
				.ForMember(dest => dest.DiscountedTotal, opts => opts.MapFrom(src => src.DiscountedTotal ?? 0))
				.ForMember(dest => dest.ETag, opts => opts.MapFrom(src => src.ETag))
				.ForMember(dest => dest.ProductId, opts => opts.MapFrom(src => StorageUtil.GetIdFromRowKey(src.RowKey, StorageConstants.ROW_ID_PRODUCT)))
				.ForMember(dest => dest.Promotions, opts => opts.MapFrom(src => StorageUtil.GetContentList<ProductPromotionEntity>(src.Promotions)))
				.ForMember(dest => dest.Quantity, opts => opts.MapFrom(src => src.Quantity ?? 0))
				.ForMember(dest => dest.Total, opts => opts.MapFrom(src => src.Total ?? 0))
				.ForMember(dest => dest.TrolleyId, opts => opts.MapFrom(src => src.PartitionKey))
				.ForMember(dest => dest.UnitPrice, opts => opts.MapFrom(src => src.UnitPrice ?? 0))
				.ForAllMembers(a => a.Condition((source, target, sourceValue, targetValue) => sourceValue != null));

			CreateMap<TrolleyProductEntity, TrolleyTableEntity>()
				.ForMember(dest => dest.Children, opts => opts.Ignore())
				.ForMember(dest => dest.DiscountedTotal, opts => opts.MapFrom(src => src.DiscountedTotal))
				.ForMember(dest => dest.ETag, opts => opts.MapFrom(src => src.ETag))
				.ForMember(dest => dest.PartitionKey, opts => opts.MapFrom(src => src.TrolleyId))
				.ForMember(dest => dest.Promotions, opts => opts.MapFrom(src => StorageUtil.GetContentString(src.Promotions)))
				.ForMember(dest => dest.Quantity, opts => opts.MapFrom(src => src.Quantity))
				.ForMember(dest => dest.RowKey, opts => opts.MapFrom(src => StorageUtil.GetRowKeyContent(new Tuple<string, string>(StorageConstants.ROW_ID_PRODUCT, src.ProductId))))
				.ForMember(dest => dest.RowType, opts => opts.MapFrom(src => StorageConstants.ROW_TYPE_PRODUCT))
				.ForMember(dest => dest.Timestamp, opts => opts.Ignore())
				.ForMember(dest => dest.Total, opts => opts.MapFrom(src => src.Total))
				.ForMember(dest => dest.UnitPrice, opts => opts.MapFrom(src => src.UnitPrice))
				.ForAllMembers(a => a.Condition((source, target, sourceValue, targetValue) => sourceValue != null));

			#endregion

			#region PromotionTableEntity - PromotionEntity

			CreateMap<PromotionTableEntity, PromotionEntity>()
				.ForMember(dest => dest.Definition, opts => opts.MapFrom(src => src.Definition))
				.ForMember(dest => dest.ETag, opts => opts.MapFrom(src => src.ETag))
				.ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
				.ForMember(dest => dest.PromotionId, opts => opts.MapFrom(src => src.PartitionKey))
				.ForMember(dest => dest.Type, opts => opts.MapFrom(src => src.Type))
				.ForAllMembers(a => a.Condition((source, target, sourceValue, targetValue) => sourceValue != null));

			CreateMap<PromotionEntity, PromotionTableEntity>()
				.ForMember(dest => dest.Definition, opts => opts.MapFrom(src => src.Definition))
				.ForMember(dest => dest.ETag, opts => opts.MapFrom(src => src.ETag))
				.ForMember(dest => dest.PartitionKey, opts => opts.MapFrom(src => src.PromotionId))
				.ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
				.ForMember(dest => dest.RowKey, opts => opts.MapFrom(src => StorageConstants.ROW_KEY_BASIC))
				.ForMember(dest => dest.Timestamp, opts => opts.Ignore())
				.ForMember(dest => dest.Type, opts => opts.MapFrom(src => src.Type))
				.ForAllMembers(a => a.Condition((source, target, sourceValue, targetValue) => sourceValue != null));

			#endregion

			#region ProductTableEntity - ProductEntity

			CreateMap<ProductTableEntity, ProductEntity>()
				.ForMember(dest => dest.CategoryId, opts => opts.MapFrom(src => src.CategoryId))
				.ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
				.ForMember(dest => dest.ProductId, opts => opts.MapFrom(src => src.PartitionKey))
				.ForMember(dest => dest.Promotions, opts => opts.MapFrom(src => StorageUtil.GetContentList<PromotionEntity>(src.Promotions)))
				.ForMember(dest => dest.UnitPrice, opts => opts.MapFrom(src => src.UnitPrice))
				.ForAllMembers(a => a.Condition((source, target, sourceValue, targetValue) => sourceValue != null));

			CreateMap<ProductEntity, ProductTableEntity>()
				.ForMember(dest => dest.CategoryId, opts => opts.MapFrom(src => src.CategoryId))
				.ForMember(dest => dest.ETag, opts => opts.MapFrom(src => src.ETag))
				.ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
				.ForMember(dest => dest.PartitionKey, opts => opts.MapFrom(src => src.ProductId))
				.ForMember(dest => dest.Promotions, opts => opts.MapFrom(src => StorageUtil.GetContentString(src.Promotions)))
				.ForMember(dest => dest.RowKey, opts => opts.MapFrom(src => StorageConstants.ROW_KEY_BASIC))
				.ForMember(dest => dest.Timestamp, opts => opts.Ignore())
				.ForMember(dest => dest.UnitPrice, opts => opts.MapFrom(src => src.UnitPrice))
				.ForAllMembers(a => a.Condition((source, target, sourceValue, targetValue) => sourceValue != null));

			#endregion
		}

		private string GetTrolleyChildrenString(TrolleyEntity trolleyEntity)
		{
			if (trolleyEntity.Products == null || trolleyEntity.Products.Count <= 0) return null;

			var children = trolleyEntity.Products
				.Select(e => new TrolleyProductEntity()
				{
					ProductId = e.ProductId
				})
				.ToList();

			return StorageUtil.GetContentString(children);
		}

		private List<TrolleyProductEntity> GetTrolleyProductList(TrolleyTableEntity tableEntity)
		{
			if (string.IsNullOrEmpty(tableEntity.Children)) return new List<TrolleyProductEntity>();

			return StorageUtil.GetContentList<TrolleyProductEntity>(tableEntity.Children);
		}
	}
}
