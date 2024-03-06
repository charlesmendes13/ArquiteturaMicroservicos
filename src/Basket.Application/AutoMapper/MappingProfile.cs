using AutoMapper;
using Basket.Application.ViewModels;
using Basket.Domain.Models;

namespace Basket.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Models.Basket, BasketViewModel>();
            CreateMap<Item, ItemViewModel>();
            CreateMap<CreateItemViewModel, Item>();
        }
    }
}
