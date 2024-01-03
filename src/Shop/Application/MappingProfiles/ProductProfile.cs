using System;
using AutoMapper;
using Shared.DTO.DTO.Products;
using Shop.Domain.Models;

namespace Shop.Application.MappingProfiles
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<CreateProduct, Product>();
            CreateMap<Product, ViewProduct>();
            CreateMap<Product, ViewProductFull>();
        }
    }
}
