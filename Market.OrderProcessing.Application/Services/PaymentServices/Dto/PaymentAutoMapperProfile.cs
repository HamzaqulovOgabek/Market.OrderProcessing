using AutoMapper;
using Market.OrderProcessing.Domain.Models;

namespace E_CommerceProjectDemo.Application.Services.PaymentServices;

public class PaymentAutoMapperProfile : Profile
{
    public PaymentAutoMapperProfile()
    {
        CreateMap<Payment, PaymentDto>()
            .ForMember(d => d.Status, cfg => cfg.MapFrom(e => e.Status.ToString()));
        CreateMap<Payment, PaymentCreateDto>();
    }
}
