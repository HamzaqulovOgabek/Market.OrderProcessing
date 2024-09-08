using AutoMapper;
using Market.Warehouse.Domain.Models;

namespace E_CommerceProjectDemo.Application.Services.CartServices;

public class CartAutoMapperProfile : Profile
{
    public CartAutoMapperProfile()
    {
        //CreateMap<CartItem, CartItemDto>()
        //    .ForMember(d => d.ProductName, cfg => cfg.MapFrom(e => e.Product.Name))
        //    .ForMember(d => d.Price, cfg => cfg.MapFrom(e => e.Product.Price))
        //    .ForMember(d => d.DiscountPrice, cfg => cfg.MapFrom(e =>
        //    e.Product.Discount.ExpirationDate > DateTime.Now ?
        //    e.Product.Price - e.Product.Price * e.Product.Discount.InterestRate / 100 : e.Product.Price));

    }
}
