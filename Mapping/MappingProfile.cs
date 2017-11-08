using AutoMapper;
using pc_store.Controllers.Resources;
using pc_store.Core.Models;

namespace pc_store.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to API
            CreateMap<Product, ProductResource>();
            CreateMap<Category, CategoryResource>();
            CreateMap<SubCategory, SubCategoryResource>();
            CreateMap<ProductPhoto, ProductPhotoResource>();
            CreateMap<SpecificationPhoto, SpecificationPhotoResource>();
            CreateMap<ShoppingCart, ShoppingCartResource>();
            CreateMap<CartItem, CartItemResource>();

            // API to Domain
            CreateMap<ProductPhotoResource, ProductPhoto>();
            CreateMap<SpecificationPhotoResource, SpecificationPhoto>();
            CreateMap<SaveProductResource, Product>()
            .ForMember(spr => spr.Id, opt => opt.Ignore());

            CreateMap<CategoryResource, Category>()
            .ForMember(cr => cr.Id, opt => opt.Ignore());

            CreateMap<SubCategoryResource, SubCategory>()
            .ForMember(scr => scr.Id, opt => opt.Ignore());

            CreateMap<SaveShoppingCartResource, ShoppingCart>()
            .ForMember(scr => scr.Id, opt => opt.Ignore());

            CreateMap<CartItemResource, CartItem>()
            .ForMember(ir => ir.Id, opt => opt.Ignore());
        }
    }
}