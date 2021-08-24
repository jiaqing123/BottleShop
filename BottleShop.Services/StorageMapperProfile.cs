using AutoMapper;
using BottleShop.Models;
using BottleShop.Storage.Entities;

namespace BottleShop.Services
{
	public class StorageMapperProfile : Profile
	{
		public StorageMapperProfile()
		{
			CreateMap<ProductEntity, Product>()
				.ForMember(dest => dest.Id, opts => opts.MapFrom(src => src.Id))
				.ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name))
				.ForMember(dest => dest.Price, opts => opts.MapFrom(src => src.Price))
				.ForAllMembers(a => a.Condition((source, target, sourceValue, targetValue) => sourceValue != null));

			CreateMap<TrolleyEntity, Trolley>()
				.ForMember(dest => dest.Products, opts => opts.MapFrom(src => src.Products))
				.ForAllMembers(a => a.Condition((source, target, sourceValue, targetValue) => sourceValue != null)); ;
		}
	}
}
