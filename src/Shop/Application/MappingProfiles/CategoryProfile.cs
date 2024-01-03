using System;
using AutoMapper;
using Shared.DTO.DTO.Categories;
using Shop.Domain.Models;

namespace Shop.Application.MappingProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CreateCategory, Category>();
            CreateMap<Category, ViewCategory>();
        }
    }
}
