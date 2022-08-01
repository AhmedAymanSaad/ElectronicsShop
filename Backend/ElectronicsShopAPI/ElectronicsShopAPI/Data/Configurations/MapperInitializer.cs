using AutoMapper;

namespace ElectronicsShopAPI.Data.Configurations
{
    public class MapperInitializer : Profile
    {
        public MapperInitializer()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Category, CreateCategoryDTO>().ReverseMap();

            CreateMap<Discount, DiscountDTO>().ReverseMap();
            
            CreateMap<DiscountType, DiscountTypeDTO>().ReverseMap();
            
            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
