using AutoMapper;
using Payment.Application.ViewModels;

namespace Payment.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreatePaymentViewModel, Domain.Models.Payment>();
        }
    }
}
